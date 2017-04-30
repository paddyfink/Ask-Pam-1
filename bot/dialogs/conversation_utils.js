var request = require('request');

var MongoClient = require('mongodb').MongoClient;

var util = require('util');

// Load ENV variables
require('dotenv').load();

exports.logConversation = function(session, luis, qna, reply_message, answered) {
  var o = {
    time : session.message.timestamp,
    conversation_id: session.message.address.conversation.id,
    org_id: "unknown",
    message: session.message.text,
    reply_message: reply_message ? reply_message: null, // null is when human should reply
    answered: (answered !== false && (qna || luis)) ? true : false,
    qna: qna ? qna : null,
    luis: luis ? luis : null
  };

  MongoClient.connect(process.env.DOCDB_CONNECTIONSTRING, function(err, db) {
    //Currently connecting each time
    // Get the collection
    var col = db.collection('bot_msg_log');
    col.insertOne(o, function(err, r) {
      // Finish up test
      db.close();
    });
  });
  
}

exports.endGlobalConversation = function (session){
  session.sendBatch();
  
  session.endConversation();
  request.post({
    url: process.env.ASKPAM_API_URL + "/api/Conversations/EndBotConversation",
    json: {
      botconversationId: session.message.address.conversation.id
    }
  }, function(error, response, body){
      console.error(util.inspect(body, false, null));
    if(error) {
      console.error(util.inspect(error, false, null));
      //TODO:  what do we do if we can't persist?
    }
  });
}
