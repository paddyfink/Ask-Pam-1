var builder = require('botbuilder');

// Load ENV variables
require('dotenv').load();

var persistBotMessage = require('./persist').persistBotMessage;

var cutils = require('./conversation_utils');
var endGlobalConversation  = cutils.endGlobalConversation;
var log = cutils.logConversation;

var util = require('util');

//=========================================================
// Bots Dialogs with LUIS
//=========================================================
// Create LUIS recognizer that points at our model and add it as the root '/' dialog for our Cortana Bot.
var model = process.env.LUIS_MODEL_URL;
var recognizer = new builder.LuisRecognizer(model);
var dialog = new builder.IntentDialog({ recognizers: [recognizer] });

dialog.matches('GoToHuman', [
  function(session, args, next){
    session.beginDialog('/human');
  }
]);

dialog.matches('Greeting', [
  function(session, args, next) {
    session.send(persistBotMessage(session, "Hi, I am Gustav. I am a bot. If I can't answer your question, \
I will pass it to my human colleagues. They are a little slower \
to repond so please be patient. How can I help you?"));
  }
])

dialog.matches('GetSpeakerSchedule', [
  function(session, args, next){
      console.log(util.inspect(session.message, false, null));
      session.userData.LOGDATA.LUIS = args;

      if(args.score < 0.5) {
        session.beginDialog('/qna');
        return;
      }

      // Resolve and store any entities passed from LUIS.
      var speaker_name = builder.EntityRecognizer.findEntity(args.entities, 'speaker name');

      session.dialogData.session_format = builder.EntityRecognizer.findEntity(args.entities, 'session format');

      if(speaker_name === undefined || speaker_name === null){
        session.dialogData.speaker_name = null;
        builder.Prompts.text(session, "What is the speaker name?");     
      } else {
        session.dialogData.speaker_name = speaker_name;
        next();
      }
  },
  function(session, results, next) {
    var sn_entity = session.dialogData.speaker_name;
    var sf_entity = session.dialogData.session_format;

    var speaker_name = (sn_entity === undefined || sn_entity === null) && results.response ? 
      results.response : sn_entity.entity;
    var session_format = sf_entity !== undefined && sf_entity !== null ? 
      sf_entity.entity : null;

    session.sendTyping();

    //TODO: remove the following code once API is implemented. 
    var answer = "";
    if(session_format){
      //TODO: Ask-Pam team to properly create an answer.
      answer = `${speaker_name}'s ${session_format} is on Wednesday at 3pm`;
    } else {
      answer = `${speaker_name} is on Wednesday at 3pm`;
    }
    session.send(persistBotMessage(session, answer));
    log(session, session.userData.LOGDATA.LUIS, null, answer, true);

    endGlobalConversation(session);
    /////

    /**TODO: Uncomment when API is integrated.
    request.get("/api/", {}, function (error, response, body){
      if(error){
        endGlobalConversation(session);
      }

      if(response && response.statusCode === 200){
        var answer = "";
        var res = JSON.parse(body);
        if(session_format){
          //TODO: Ask-Pam team to properly create an answer.
          answer = `${speaker_name}'s ${session_format} is on Wednesday, bablbaalala`;
        } else {
          answer = `${speaker_name} is on at blalbala`;
        }

        session.send(persistBotMessage(session, answer));
        endGlobalConversation(session);
      } else {
        session.send(persistBotMessage(session, "I couldn't find that speaker, are you sure of the name?");
        endGlobalConversation(session);
      }
    });
    */

  }
]);

dialog.matches('GetEventTopic', [
  function(session, args, next){
      session.userData.LOGDATA.LUIS = args;
      if(args.score < 0.5) {
        session.beginDialog('/qna');
        return;
      }

      // Resolve and store any entities passed from LUIS.
      var topic = builder.EntityRecognizer.findEntity(args.entities, 'topic');
      if(topic === undefined || topic === null){
        session.dialogData.topic = null;
        builder.Prompts.text(session, "What is the topic?");     
      } else {
        session.dialogData.topic = topic;
        next();
      }
  },
  function(session, results, next) {
    var t_entity = session.dialogData.topic;

    var topic = (t_entity === undefined || t_entity === null) && results.response ? 
      results.response : t_entity.entity;

    session.sendTyping();

    //TODO: remove the following code once API is implemented. 
    var answer = `We found the following sessions that match ${topic}:\n1.BLABLABA`;
    session.send(persistBotMessage(session, answer));
    log(session, session.userData.LOGDATA.LUIS, null, answer, true);

    endGlobalConversation(session);
    /////

    /**TODO: Uncomment when API is integrated.
    request.get("/api/", {}, function (error, response, body){
      if(error){
        endGlobalConversation(session);
      }

      if(response && response.statusCode === 200){
        var answer = "";
        var res = JSON.parse(body);

        //TODO: Ask-Pam team to properly create an answer.
        //TODO: Switch to cards in the future.
        var answer = `We found the following sessions that match ${topic}:\n1.BLABLABA`;

        session.send(persistBotMessage(session, answer));
        endGlobalConversation(session);
      } else {
        session.send(persistBotMessage(session, "I couldn't find anything that matched this topic."));
        endGlobalConversation(session);
      }
    });
    */

  }
]);

dialog.matches('GetSpeakerInfo', [
  function(session, args, next){
      session.userData.LOGDATA.LUIS = args;
      if(args.score < 0.5) {
        session.beginDialog('/qna');
        return;
      }

      // Resolve and store any entities passed from LUIS.
      var speaker_name = builder.EntityRecognizer.findEntity(args.entities, 'speaker name');
      if(speaker_name === undefined || speaker_name === null){
        session.dialogData.speaker_name = null;
        builder.Prompts.text(session, "What is the speaker name?");     
      } else {
        session.dialogData.speaker_name = speaker_name;
        next();
      }
  },
  function(session, results, next) {
    var sn_entity = session.dialogData.topic;

    var speaker = (sn_entity === undefined || sn_entity === null) && results.response ? 
      results.response : sn_entity.entity;

    session.sendTyping();

    //TODO: remove the following code once API is implemented. 
    var answer = `${speaker}: info \n1.BLABLABA`;
    session.send(persistBotMessage(session, answer));
    log(session, session.userData.LOGDATA.LUIS, null, answer, true);

    endGlobalConversation(session);
    /////

    /**TODO: Uncomment when API is integrated.
    request.get("/api/", {}, function (error, response, body){
      if(error){
        endGlobalConversation(session);
      }

      if(response && response.statusCode === 200){
        var answer = "";
        var res = JSON.parse(body);

        //TODO: Ask-Pam team to properly create an answer.
        //TODO: Switch to cards in the future.
        var answer = `${speaker}: info \n1.BLABLABA`;

        session.send(persistBotMessage(session, answer));
        endGlobalConversation(session);
      } else {
        session.send(persistBotMessage(session, "I couldn't find anything that matched this topic."));
        endGlobalConversation(session);
      }
    });
    */

  }
]);

// Intent - Get Event Location
dialog.matches('GetEventLocation', [
  function(session, args, next){
    session.userData.LOGDATA.LUIS = args;
    if(args.score < 0.5) {
      session.beginDialog('/qna');
      return;
    }

    var en_entity = builder.EntityRecognizer.findEntity(args.entities, 'event name');
    var sf_entity = builder.EntityRecognizer.findEntity(args.entities, 'session format');
    var sn_entity = builder.EntityRecognizer.findEntity(args.entities, 'session name');
    var xl_entity = builder.EntityRecognizer.findEntity(args.entities, 'x location');

    var event_name = en_entity ? en_entity.entity : null;
    var session_format = sf_entity ? sf_entity.entity : null;
    var session_name = sn_entity ? sn_entity.entity : null;
    var x_location = xl_entity ? xl_entity.entity : null;

    session.sendTyping();

    //TODO: remove the following code once API is implemented.
    if(event_name || session_format || session_name || x_location) {
      var answer = "here's your answer";
      session.send(persistBotMessage(session, answer));
      log(session, session.userData.LOGDATA.LUIS, null, answer, true);

      endGlobalConversation(session);
    } else {
      // We don't know the answer so send to a human.
      session.replaceDialog('/qna');
      
    }
    /////

    /**TODO: Uncomment when API is integrated.
    request.get("/api/", {}, function (error, response, body){
      if(error){
        endGlobalConversation(session);
      }

      if(response && response.statusCode === 200){
        var answer = "";
        var res = JSON.parse(body);
        if(session_format){
          //TODO: Ask-Pam team to properly create an answer.
          answer = `${speaker_name}'s ${session_format} is on Wednesday, bablbaalala`;
        } else {
          answer = `${speaker_name} is on at blalbala`;
        }

        session.send(persistBotMessage(session, answer));
        endGlobalConversation(session);
      } else {
        session.send(persistBotMessage(session, "I couldn't find that speaker, are you sure of the name?"));
        endGlobalConversation(session);
      }
    });
    */

  }
]);

dialog.onBegin(function(session, args, next){
  session.userData.LOGDATA = {};
  next();
})
// If LUIS doesn't recognize the intent, go to the QNA maker. 
dialog.onDefault(function (session, args, next) {
    session.userData.LOGDATA.LUIS = args;
    session.beginDialog('/qna');
});

module.exports = dialog;

