var builder = require('botbuilder');
var request = require('request');

var persistBotMessage = require('./persist').persistBotMessage;

var cutils = require('./conversation_utils');
var endGlobalConversation  = cutils.endGlobalConversation;
var log = cutils.logConversation;

// Load ENV variables
require('dotenv').load();

//=========================================================
// Bots Dialogs with QA
//=========================================================

module.exports = [
    function (session, args, next) {
      session.sendTyping();

      var q = session.message.text;

      request.post({
        method: "post",
        url: process.env.QNAMAKER_URL + "/knowledgebases/" + process.env.QNAMAKER_KBID + "/generateAnswer", 
        headers: {
          "Ocp-Apim-Subscription-Key": process.env.QNAMAKER_SUBKEY
        },
        json: {question: q}
      }, function(error, response, body){
        if(error || body.score < 50) {
          // We don't know the answer so send to a human.
          session.userData.LOGDATA.QNA = body;
          session.replaceDialog('/human');
          return;
        } else {
          session.send(persistBotMessage(session, body.answer));
          log(session, session.userData.LOGDATA.LUIS, body, body.answer, true);
          endGlobalConversation(session);
        }
      });
    }
];
