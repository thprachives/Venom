const mongoose = require('mongoose');
const { Schema } = mongoose;

const scoreSchema = new Schema({
    username: String,
    score: String,
    scoreAI: String,
    lastAuthentication: Date,
});

mongoose.model('score', scoreSchema);