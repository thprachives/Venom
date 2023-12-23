const mongoose = require('mongoose');
const Score = mongoose.model('score');

module.exports = app => {
    app.get('/score', async (req, res) => {

        //var response = {};

        const { rUsername, rScore, rScoreAI } = req.query;
        console.log(rUsername);
        console.log(rScore, rScoreAI);
s
        if (rUsername == null || rScore == null || rScoreAI == null) {
            //response.code = 1;
            //response.msg = "Invalid crededential"
            //res.send(response);

            console.log("Invalid credentials");
            return res.send("Invalid Credential");
        }

        var userScore = await Score.findOne({ username: rUsername });
        if (userScore == null) {
            console.log("Save Score...");

            var newScore = new Score({
                username: rUsername,
                score: rScore,
                scoreAI: rScoreAI,
                lastAuthentication: Date.now()
            });
            await newScore.save();

            //response.code = 0;
            //response.msg = "Account found";
            //response.data = userScore;
            //res.send(response);
            return res.send(newScore);
        } else{
            console.log("Score already submitted");
            return res.send(rScore);
        }
        //res.send("Invalid credentials");
        //return;
    });
}