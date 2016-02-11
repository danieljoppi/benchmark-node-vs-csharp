'use strict';

var Q = require('q'),
    db = require('./db-config');

exports.processTransaction = (req, res) => {
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
    }
    var value1 = values[0] || 1,
        value2 = values[1] || 1;

    let tag1 = db.findTag(key1);
    let tag2 = db.findTag(key2);
    Q.all([tag1, tag2]).then((vals) => {
        var facts = vals || [],
            // reference to tag 1
            fact1 = facts[0] || 1,
            // reference to tag 2
            fact2 = facts[1] || 1;

        values.push(value1 * fact1 + value2 * fact2);

        res.status(200).send(body);

        db.insertTransaction(body);
    })
    .catch((err) => {
        res.status(500).send(err);
    });
};