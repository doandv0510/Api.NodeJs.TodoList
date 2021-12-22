const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');
const mongoose = require('mongoose');

const port = 8000;

const app = express();

const todoRoutes = require('./routes/Todo');
app.use("/api", todoRoutes)

app.use(cors());
app.use(bodyParser.json());

app.listen(port,()=>{
    console.log(`Listening to http://localhost:${port}`);
})