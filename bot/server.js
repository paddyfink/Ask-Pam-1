// Load ENV variables
require('dotenv').load();

var restify = require('restify');

// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function() {
    console.log('%s listening to %s', server.name, server.url);
});

//=========================================================
// Bot Setup
//=========================================================

var builder = require('botbuilder');
var luisDialog = require('./dialogs/luis');
var qnaDialog = require('./dialogs/qna');
var humanDialog = require('./dialogs/human');

// Create chat bot
var connector = new builder.ChatConnector({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

var bot = new builder.UniversalBot(connector);
bot.dialog('/', luisDialog);
bot.dialog('/qna', qnaDialog);
bot.dialog('/human', humanDialog);

server.post('/api/messages', connector.listen());