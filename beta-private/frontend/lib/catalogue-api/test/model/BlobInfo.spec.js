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
    instance = new CatalogueApi.BlobInfo();
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

  describe('BlobInfo', function() {
    it('should create an instance of BlobInfo', function() {
      // uncomment below and update the code to test BlobInfo
      //var instance = new CatalogueApi.BlobInfo();
      //expect(instance).to.be.a(CatalogueApi.BlobInfo);
    });

    it('should have the property id (base name: "id")', function() {
      // uncomment below and update the code to test the property id
      //var instance = new CatalogueApi.BlobInfo();
      //expect(instance).to.be();
    });

    it('should have the property parentId (base name: "parentId")', function() {
      // uncomment below and update the code to test the property parentId
      //var instance = new CatalogueApi.BlobInfo();
      //expect(instance).to.be();
    });

    it('should have the property name (base name: "name")', function() {
      // uncomment below and update the code to test the property name
      //var instance = new CatalogueApi.BlobInfo();
      //expect(instance).to.be();
    });

    it('should have the property isFolder (base name: "isFolder")', function() {
      // uncomment below and update the code to test the property isFolder
      //var instance = new CatalogueApi.BlobInfo();
      //expect(instance).to.be();
    });

    it('should have the property length (base name: "length")', function() {
      // uncomment below and update the code to test the property length
      //var instance = new CatalogueApi.BlobInfo();
      //expect(instance).to.be();
    });

    it('should have the property url (base name: "url")', function() {
      // uncomment below and update the code to test the property url
      //var instance = new CatalogueApi.BlobInfo();
      //expect(instance).to.be();
    });

    it('should have the property timeLastModified (base name: "timeLastModified")', function() {
      // uncomment below and update the code to test the property timeLastModified
      //var instance = new CatalogueApi.BlobInfo();
      //expect(instance).to.be();
    });

    it('should have the property blobId (base name: "blobId")', function() {
      // uncomment below and update the code to test the property blobId
      //var instance = new CatalogueApi.BlobInfo();
      //expect(instance).to.be();
    });

  });

}));
