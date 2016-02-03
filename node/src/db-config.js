'use strict';
var cassandra = require('cassandra-driver');

var client = new cassandra.Client({keyspace: 'benchmark'});

exports.insertTransaction = function (data) {
    
};

exports.findTag = function (key) {
    // var query = 'SELECT email, last_name FROM user_profiles WHERE key=?';
    // client.execute(query, ['guy'], function(err, result) {
    //   assert.ifError(err);
    //   console.log('got user profile with email ' + result.rows[0].email);
    // });
};