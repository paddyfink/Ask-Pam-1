var persistBotMessage = require('./persist').persistBotMessage;

var cutils = require('./conversation_utils');
var endGlobalConversation  = cutils.endGlobalConversation;
var log = cutils.logConversation;
//=========================================================
// Bots Dialogs with Human
//=========================================================

module.exports = [
    function (session, args, next) {
      session.send(persistBotMessage(session, "Sorry, I can't help you with that. Let me find one of my human colleagues. Please hold on."));
      log(session, session.userData.LOGDATA.LUIS, session.userData.LOGDATA.QNA, null, false);
      //TODO: TOGGLE HUMAN API CALL.
      endGlobalConversation(session);
    }
];
