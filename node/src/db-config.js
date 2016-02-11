'use strict';
var cassandra = require('cassandra-driver');
var uuid = require('node-uuid');
var Q = require('q');

var client = new cassandra.Client({contactPoints: ['127.0.0.1'], keyspace: 'benchmark'});

exports.insertTransaction = (data, cb) => {
    var json = {
        // generate UUID
        code: uuid.v1(),
        tags: data.tags,
        values: data.values,
        address: data.address
    };
    
    let deferred = Q.defer(),
        query = [{
            query: 'insert into transactions JSON ?',
            params: [JSON.stringify(json)]
        }];
    client.batch(query, {prepare: true}, (err) => {
        if (err) {
            deferred.reject(new Error(err));
            console.error(err);
        } else {
            deferred.resolve();
        }
    });
    return deferred.promise;
};

exports.findTag = function (key) {
    let deferred = Q.defer(),
        query = 'select value from tags where key = ?';
    client.execute(query, [key], (err, result) => {
        if (err) {
            deferred.reject(new Error(err));
            console.error(err);
        } else {
            deferred.resolve(result.rows[0].value);
        }
    });
    return deferred.promise;
};