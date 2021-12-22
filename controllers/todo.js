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
    Todo.find()
    .sort("createdAt")
    .exec((err, todos)=>{
        if(err || !todos){
            return res.status(400).json(
                {
                    error: "something went wrong in finding all todos" 
                });
        }
        else{
            res.json(todos);
            // next();
        }
    });
};

exports.GetById=(req,res,next,todoId)=>{
    Todo.findById(todoId).exec((err, todo) => {
        if (err || !todo) {
            return res.status(400).json({ error: "404 todo not found" });
        }
        req.todo = todo;
        console.log(req.todo);
        next();
    });
};

exports.GetTodo = (req, res) => {
    return res.json(req.todo);
};

exports.updateTodo = (req, res) => {
    const todo = req.todo;

    todo.task = req.body.task;

    todo.save((err, task) => {
        if (err || !task) {
            return res
                .status(400)
                .json({ error: "something went wrong while updating" });
        }
        res.json({ task });
    });
};

exports.DeleteTodo = (req, res) => {
    const todo = req.todo;

    todo.remove((err, task) => {
        if (err || !task) {
            return res
                .status(400)
                .json({ error: "something went wrong while deteting the todo" });
        }
        res.json({
            message: "Todo deleted successfully!",
        });
    });
};