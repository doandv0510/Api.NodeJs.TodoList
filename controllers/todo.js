const Todo = require("../models/todo");

exports.createTodo=(req,res)=>{
    const todo = new Todo(req.body);

    todo.save((err, task) => {
        if (err || !task) {
            return res.status(400).json({ error: "something went wrong" });
        }
        res.json({ task });
    });
}

exports.getAllTodos=(req,res)=>{
    Todo.find().sort("createdAt").exec((err, todos)=>{
        if(err || !todos){
            return res.status(400).json(
                {
                    error: "something went wrong in finding all todos" 
                });
        }
        else{
            res.json(todos);
        }
    });
}