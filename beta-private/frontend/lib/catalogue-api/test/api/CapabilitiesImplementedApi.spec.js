/**
 * catalogue-api
 * NHS Digital GP IT Futures Buying Catalog API
 *
 * OpenAPI spec version: 1.0.0-private-beta
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 *
 * Swagger Codegen version: 2.4.0-SNAPSHOT
 *
 * Do not edit the class manually.
 *
 */

(function(root, factory) {
  if (typeof define === 'function' && define.amd) {
    // AMD.
    define(['expect.js', '../../src/index'], factory);
  } else if (typeof module === 'object' && module.exports) {
    // CommonJS-like environments that support module.exports, like Node.
    factory(require('expect.js'), require('../../src/index'));
  } else {
    // Browser globals (root is window)
    factory(root.expect, root.CatalogueApi);
  }
}(this, function(expect, CatalogueApi) {
  'use strict';

  var instance;

  beforeEach(function() {
    instance = new CatalogueApi.CapabilitiesImplementedApi();
  });

  var getProperty = function(object, getter, property) {
    // Use getter method if present; otherwise, get the property directly.
    if (typeof object[getter] === 'function')
      return object[getter]();
    else
      return object[property];
  }

  var setProperty = function(object, setter, property, value) {
    // Use setter method if present; otherwise, set the property directly.
    if (typeof object[setter] === 'function')
      object[setter](value);
    else
      object[property] = value;
  }

  describe('CapabilitiesImplementedApi', function() {
    describe('apiCapabilitiesImplementedByIdGet', function() {
      it('should call apiCapabilitiesImplementedByIdGet successfully', function(done) {
        //uncomment below and update the code to test apiCapabilitiesImplementedByIdGet
        //instance.apiCapabilitiesImplementedByIdGet(function(error) {
        //  if (error) throw error;
        //expect().to.be();
        //});
        done();
      });
    });
    describe('apiCapabilitiesImplementedBySolutionBySolutionIdGet', function() {
      it('should call apiCapabilitiesImplementedBySolutionBySolutionIdGet successfully', function(done) {
        //uncomment below and update the code to test apiCapabilitiesImplementedBySolutionBySolutionIdGet
        //instance.apiCapabilitiesImplementedBySolutionBySolutionIdGet(function(error) {
        //  if (error) throw error;
        //expect().to.be();
        //});
        done();
      });
    });
    describe('apiCapabilitiesImplementedDelete', function() {
      it('should call apiCapabilitiesImplementedDelete successfully', function(done) {
        //uncomment below and update the code to test apiCapabilitiesImplementedDelete
        //instance.apiCapabilitiesImplementedDelete(function(error) {
        //  if (error) throw error;
        //expect().to.be();
        //});
        done();
      });
    });
    describe('apiCapabilitiesImplementedPost', function() {
      it('should call apiCapabilitiesImplementedPost successfully', function(done) {
        //uncomment below and update the code to test apiCapabilitiesImplementedPost
        //instance.apiCapabilitiesImplementedPost(function(error) {
        //  if (error) throw error;
        //expect().to.be();
        //});
        done();
      });
    });
    describe('apiCapabilitiesImplementedPut', function() {
      it('should call apiCapabilitiesImplementedPut successfully', function(done) {
        //uncomment below and update the code to test apiCapabilitiesImplementedPut
        //instance.apiCapabilitiesImplementedPut(function(error) {
        //  if (error) throw error;
        //expect().to.be();
        //});
        done();
      });
    });
  });

}));
