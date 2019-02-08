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
    // AMD. Register as an anonymous module.
    define(['ApiClient'], factory);
  } else if (typeof module === 'object' && module.exports) {
    // CommonJS-like environments that support module.exports, like Node.
    module.exports = factory(require('../ApiClient'));
  } else {
    // Browser globals (root is window)
    if (!root.CatalogueApi) {
      root.CatalogueApi = {};
    }
    root.CatalogueApi.Contacts = factory(root.CatalogueApi.ApiClient);
  }
}(this, function(ApiClient) {
  'use strict';




  /**
   * The Contacts model module.
   * @module model/Contacts
   * @version 1.0.0-private-beta
   */

  /**
   * Constructs a new <code>Contacts</code>.
   * A means of communicating with an organisation, typically a person, email address, telephone number etc.  Standard MS Dynamics CRM entity
   * @alias module:model/Contacts
   * @class
   * @param id {String} Unique identifier of entity
   * @param organisationId {String} Unique identifier of organisation
   * @param emailAddress1 {String} Primary email address of contact
   */
  var exports = function(id, organisationId, emailAddress1) {
    var _this = this;

    _this['id'] = id;
    _this['organisationId'] = organisationId;


    _this['emailAddress1'] = emailAddress1;

  };

  /**
   * Constructs a <code>Contacts</code> from a plain JavaScript object, optionally creating a new instance.
   * Copies all relevant properties from <code>data</code> to <code>obj</code> if supplied or a new instance if not.
   * @param {Object} data The plain JavaScript object bearing properties of interest.
   * @param {module:model/Contacts} obj Optional instance to populate.
   * @return {module:model/Contacts} The populated <code>Contacts</code> instance.
   */
  exports.constructFromObject = function(data, obj) {
    if (data) {
      obj = obj || new exports();

      if (data.hasOwnProperty('id')) {
        obj['id'] = ApiClient.convertToType(data['id'], 'String');
      }
      if (data.hasOwnProperty('organisationId')) {
        obj['organisationId'] = ApiClient.convertToType(data['organisationId'], 'String');
      }
      if (data.hasOwnProperty('firstName')) {
        obj['firstName'] = ApiClient.convertToType(data['firstName'], 'String');
      }
      if (data.hasOwnProperty('lastName')) {
        obj['lastName'] = ApiClient.convertToType(data['lastName'], 'String');
      }
      if (data.hasOwnProperty('emailAddress1')) {
        obj['emailAddress1'] = ApiClient.convertToType(data['emailAddress1'], 'String');
      }
      if (data.hasOwnProperty('phoneNumber1')) {
        obj['phoneNumber1'] = ApiClient.convertToType(data['phoneNumber1'], 'String');
      }
    }
    return obj;
  }

  /**
   * Unique identifier of entity
   * @member {String} id
   */
  exports.prototype['id'] = undefined;
  /**
   * Unique identifier of organisation
   * @member {String} organisationId
   */
  exports.prototype['organisationId'] = undefined;
  /**
   * First name of contact
   * @member {String} firstName
   */
  exports.prototype['firstName'] = undefined;
  /**
   * Last name of contact
   * @member {String} lastName
   */
  exports.prototype['lastName'] = undefined;
  /**
   * Primary email address of contact
   * @member {String} emailAddress1
   */
  exports.prototype['emailAddress1'] = undefined;
  /**
   * Primary phone number of contact
   * @member {String} phoneNumber1
   */
  exports.prototype['phoneNumber1'] = undefined;



  return exports;
}));


