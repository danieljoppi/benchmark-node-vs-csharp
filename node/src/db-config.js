'use strict';
var cassandra = require('cassandra-driver');
var uuid = require('node-uuid');

var client = new cassandra.Client({contactPoints: ['127.0.0.1'], keyspace: 'benchmark'});

exports.insertTransaction = (data, cb) => {
    // generate UUID
    data.code = uuid.v1();
    
    var query = [{
        query: 'insert into transactions JSON ?',
        params: [JSON.stringify(data)]
    }];
    client.batch(query, {prepare: true}, (err) => {
      if (err) {
          console.error(err);
      }
      cb();
    });
};

exports.findTag = function (key, cb) {
    var query = 'select value from tags where key = ?';
    client.execute(query, [key], (err, result) => {
        if (err) {
            console.error(err);
            cb();
        } else {
            cb(result.rows[0].value);
        }
    });
};