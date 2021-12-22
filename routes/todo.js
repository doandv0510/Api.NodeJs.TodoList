const express = require('express');
const router = express.Router();
const {getAllTodos} = require('../controllers/Todo') 
router.get("/todos/", getAllTodos); 
module.exports = router;