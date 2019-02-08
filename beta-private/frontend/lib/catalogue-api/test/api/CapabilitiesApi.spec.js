/**
 * catalogue-api
 * NHS Digital GP IT Futures Buying Catalog API
 *
 * OpenAPI spec version: 1.0.0-private-beta
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 *
 * Swagger Codegen version: 2.4.2-SNAPSHOT
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
    instance = new CatalogueApi.CapabilitiesApi();
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

  describe('CapabilitiesApi', function() {
    describe('apiCapabilitiesByFrameworkByFrameworkIdGet', function() {
      it('should call apiCapabilitiesByFrameworkByFrameworkIdGet successfully', function(done) {
        //uncomment below and update the code to test apiCapabilitiesByFrameworkByFrameworkIdGet
        //instance.apiCapabilitiesByFrameworkByFrameworkIdGet(function(error) {
        //  if (error) throw error;
        //expect().to.be();
        //});
        done();
      });
    });
    describe('apiCapabilitiesByIdByIdGet', function() {
      it('should call apiCapabilitiesByIdByIdGet successfully', function(done) {
        //uncomment below and update the code to test apiCapabilitiesByIdByIdGet
        //instance.apiCapabilitiesByIdByIdGet(function(error) {
        //  if (error) throw error;
        //expect().to.be();
        //});
        done();
      });
    });
    describe('apiCapabilitiesByIdsPost', function() {
      it('should call apiCapabilitiesByIdsPost successfully', function(done) {
        //uncomment below and update the code to test apiCapabilitiesByIdsPost
        //instance.apiCapabilitiesByIdsPost(function(error) {
        //  if (error) throw error;
        //expect().to.be();
        //});
        done();
      });
    });
    describe('apiCapabilitiesByStandardByStandardIdGet', function() {
      it('should call apiCapabilitiesByStandardByStandardIdGet successfully', function(done) {
        //uncomment below and update the code to test apiCapabilitiesByStandardByStandardIdGet
        //instance.apiCapabilitiesByStandardByStandardIdGet(function(error) {
        //  if (error) throw error;
        //expect().to.be();
        //});
        done();
      });
    });
    describe('apiCapabilitiesGet', function() {
      it('should call apiCapabilitiesGet successfully', function(done) {
        //uncomment below and update the code to test apiCapabilitiesGet
        //instance.apiCapabilitiesGet(function(error) {
        //  if (error) throw error;
        //expect().to.be();
        //});
        done();
      });
    });
  });

}));
