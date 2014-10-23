var exec = require('cordova/exec');

exports.captureScreen = function(success, error) {
    exec(success, error, "Rapture", "captureScreen", []);
};
