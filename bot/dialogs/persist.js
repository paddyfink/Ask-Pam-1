var request = require('request');

exports.persistBotMessage = function(session, message){
  request.post({
    url: process.env.ASKPAM_API_URL + "/api/Conversations/PostBotMessage",
    json: {
      botconversationId: session.message.address.conversation.id,
      message: message
    }
  }, function(error, response, body){
    if(error) {
      console.error(error);
      //TODO:  what do we do if we can't persist?
    }
  });
  return message;
}
