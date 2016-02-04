'use strict';

var db = require('./db-config');

exports.post = (req, res) => {
    var body = req.body;
    
    var tags = body.tags;
    if (!tags || tags.length < 2) {
        body.tags = [
            'legal',
            'opa'
        ];
        tags = body.tags;
    }
    
    var key1 = tags[0],
        key2 = tags[1];
    
    var values = body.values;
    if (!values || values.length < 2) {
        body.values = [];
    } else {
        var value1 = values[0],
            value2 = values[1];
        
        var fact1 = 1,
            fact2 = 1;
        var actual = 0,
            verify = () => {
                if (++actual === 2) {
                    values.push(value1 * fact1 + value2 * fact2); 
                    
                    db.insertTransaction(body, function() { 
                        res.status(200).send(body);
                    });
                }
            };
       
       db.findTag(key1, val => verify(fact1 = val));
       db.findTag(key2, val => verify(fact2 = val));
    }
};