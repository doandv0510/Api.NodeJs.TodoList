const express = require('express');
const { mongo} = require("mongoose"); 
const router = express.Router();

const {
    getAllTodos, 
    createTodo,
    GetById,
    GetTodo,
    updateTodo,
    DeleteTodo
} = require('../controllers/todo'); 

const strLink = "/Todo/";
router.get(strLink , getAllTodos); 

router.param("todoId", GetById); 

router.post(strLink + "Create", createTodo); 

router.get(strLink + "GetById/:todoId", GetTodo); 

router.put("/todo/:todoId/update", updateTodo);

router.delete("/todo/delete/:todoId", DeleteTodo); 

module.exports = router;