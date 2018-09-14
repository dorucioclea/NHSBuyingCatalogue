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
    root.CatalogueApi.Organisations = factory(root.CatalogueApi.ApiClient);
  }
}(this, function(ApiClient) {
  'use strict';




  /**
   * The Organisations model module.
   * @module model/Organisations
   * @version 1.0.0-private-beta
   */

  /**
   * Constructs a new <code>Organisations</code>.
   * A &#39;Supplier&#39; is a company who supplies ‘solutions’ to NHS buyers.  A &#39;Buyer&#39; is a company who purchases &#39;solutions&#39; from a &#39;Supplier&#39;.  An &#39;Admin&#39; is a company, typically NHS Digital, who has ultimate control over all information in the Buying Catalog.  Standard MS Dynamics CRM entity
   * @alias module:model/Organisations
   * @class
   * @param id {String} Unique identifier of entity
   * @param name {String} Name of a company, as displayed to the user
   * @param odsCode {String} ODS code for company
   * @param primaryRoleId {String} Primary ODS role code for company  Suppliers have a primary role of \"RO92\"
   * @param status {String} Operational status of company  Typically:  \"Active\", \"Inactive\"
   */
  var exports = function(id, name, odsCode, primaryRoleId, status) {
    var _this = this;

    _this['id'] = id;
    _this['name'] = name;
    _this['odsCode'] = odsCode;
    _this['primaryRoleId'] = primaryRoleId;
    _this['status'] = status;

  };

  /**
   * Constructs a <code>Organisations</code> from a plain JavaScript object, optionally creating a new instance.
   * Copies all relevant properties from <code>data</code> to <code>obj</code> if supplied or a new instance if not.
   * @param {Object} data The plain JavaScript object bearing properties of interest.
   * @param {module:model/Organisations} obj Optional instance to populate.
   * @return {module:model/Organisations} The populated <code>Organisations</code> instance.
   */
  exports.constructFromObject = function(data, obj) {
    if (data) {
      obj = obj || new exports();

      if (data.hasOwnProperty('id')) {
        obj['id'] = ApiClient.convertToType(data['id'], 'String');
      }
      if (data.hasOwnProperty('name')) {
        obj['name'] = ApiClient.convertToType(data['name'], 'String');
      }
      if (data.hasOwnProperty('odsCode')) {
        obj['odsCode'] = ApiClient.convertToType(data['odsCode'], 'String');
      }
      if (data.hasOwnProperty('primaryRoleId')) {
        obj['primaryRoleId'] = ApiClient.convertToType(data['primaryRoleId'], 'String');
      }
      if (data.hasOwnProperty('status')) {
        obj['status'] = ApiClient.convertToType(data['status'], 'String');
      }
      if (data.hasOwnProperty('description')) {
        obj['description'] = ApiClient.convertToType(data['description'], 'String');
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
   * Name of a company, as displayed to the user
   * @member {String} name
   */
  exports.prototype['name'] = undefined;
  /**
   * ODS code for company
   * @member {String} odsCode
   */
  exports.prototype['odsCode'] = undefined;
  /**
   * Primary ODS role code for company  Suppliers have a primary role of \"RO92\"
   * @member {String} primaryRoleId
   */
  exports.prototype['primaryRoleId'] = undefined;
  /**
   * Operational status of company  Typically:  \"Active\", \"Inactive\"
   * @member {String} status
   */
  exports.prototype['status'] = undefined;
  /**
   * Information about company
   * @member {String} description
   */
  exports.prototype['description'] = undefined;



  return exports;
}));


