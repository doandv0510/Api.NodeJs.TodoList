const express = require('express');
const { mongo} = require("mongoose"); 
const router = express.Router();

const {
    getAllTodos, 
    createTodo
} = require('../controllers/todo'); 

const strLink = "/Todo/";
router.get(strLink , getAllTodos); 

router.post(strLink + "Create", createTodo); 

module.exports = router;