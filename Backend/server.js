// const express = require('express');
// const keys = require('./config/keys.js');
// const app = express();
// const bodyParser = require('body-parser');
// const dotenv = require("dotenv");

// // Add middleware for JSON parsing
// //app.use(express.json());


// //dotenv.config({ path: "config.env"});
// //require("dotenv").config({ path: "./config.env" });

// //parser application/x-www-form-urlencoded
// app.use(express.json());
// //app.use(express.urlencoded());
// app.use(bodyParser.urlencoded({ extended: false }));
// dotenv.config();

// //Setting up DB
// const mongoose = require('mongoose');
// mongoose.connect("mongodb+srv://sainisania:9CKtgtgybVk0QDlU@cluster0.osl86tn.mongodb.net/?retryWrites=true&w=majority" , { useNewUrlParser: true, useUnifiedTopology: true });

// //Setup database models
// require('./model/Account');
// require('./model/Score');

// //setup routes
// require('./routes/authenticationROUTE')(app);
// require('./routes/ScoreAuth')(app);

// //const port= 13777;
// app.listen(keys.port, () => {
//     console.log("Listening on " + keys.port);
// });

const express = require('express');
const bodyParser = require('body-parser');
const mongoose = require('mongoose');
const dotenv = require('dotenv');
const keys = require('./config/keys.js');

// Create Express app
const app = express();

// Load environment variables from .env file
dotenv.config();

// Parse JSON bodies (as sent by API clients)
app.use(express.json());
app.use(bodyParser.urlencoded({ extended: false }));


// Connect to MongoDB
mongoose.connect(
  "mongodb+srv://sainisania:9CKtgtgybVk0QDlU@cluster0.osl86tn.mongodb.net/Venom-backend",
  { useNewUrlParser: true, useUnifiedTopology: true }
);

const db = mongoose.connection;

// Listen for MongoDB connection events
db.on('error', (err) => {
  console.error('MongoDB connection error:', err);
});

db.once('open', () => {
  console.log('Connected to MongoDB');
});

// Load the models
require('./model/Account');
require('./model/Score');
require('./routes/authenticationROUTE')(app);
require('./routes/ScoreAuth')(app);

// Set the port number
const port = keys.port;

// Start the server and listen on the specified port
app.listen(port, () => {
  console.log("Listening on " + port);
});