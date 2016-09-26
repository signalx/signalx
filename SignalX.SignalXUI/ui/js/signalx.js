(function($, window, undefined) {
    var signalx = {};
    var handlers = {};
    signalx.server = false;
    var hasRun = false;
    var mailBox = [];
    mailBox.run = function() {
        if (signalx.server) {
            while (mailBox.length) {
                var func = mailBox.pop();
                func(signalx.server);
            }
        }
    };
    var toCamelCase = function (str) {
        return str.charAt(0).toLowerCase() + str.slice(1);
    };
    var toUnCamelCase = function (str) {
        return str.charAt(0).toUpperCase() + str.slice(1);
    };
    signalx.client = function(name, f) {
        //todo check if is function
        if (name && f) {
            handlers[name] = f;
            var camelCase = toCamelCase(name);
            if (camelCase !== name) {
                handlers[camelCase] = f;
            }

            var unCamelCase = toUnCamelCase(name);
            if (unCamelCase !== name) {
                handlers[unCamelCase] = f;
            }
        } else {
            throw "Please supply a va;id client handler name and method";
        }
    };

    signalx.ready = function(f) {
        mailBox.push(f);
        mailBox.run();
        if (!hasRun) {
            hasRun = true;
            $.ajax({
                url: "/signalr/hubs",
                dataType: "script",
                success: function() {
                    $(function() {
                        var chat = $.connection.signalXHub;

                        for (var key in signalx.client) {
                            if (signalx.client.hasOwnProperty(key)) {
                                handlers[key] = signalx.client[key];
                                var camelCase = toCamelCase(key);
                                if (camelCase !== key) {
                                    handlers[camelCase] = signalx.client[key];
                                }

                                var unCamelCase = toUnCamelCase(key);
                                if (unCamelCase !== key) {
                                    handlers[unCamelCase] = signalx.client[key];
                                }
                            }
                        }
                        chat.client.addMessage = function(message) {
                            signalx.server = eval(message);
                            mailBox.run();
                        };

                        chat.client.broadcastMessage = function(owner, message) {
                            var own = handlers[owner];
                            own && own(message);
                        };
                        var promise = $.connection.hub.start();
                        promise.done(function() {
                            chat.server.getMethods();
                        });
                    });
                }
            });
        }
    };
    window.signalx = signalx;
    setTimeout(function() {
        signalx.ready(function() {

        });
    }, 0);
}(window.jQuery, window));