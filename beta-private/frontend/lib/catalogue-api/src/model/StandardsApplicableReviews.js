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
    root.CatalogueApi.StandardsApplicableReviews = factory(root.CatalogueApi.ApiClient);
  }
}(this, function(ApiClient) {
  'use strict';




  /**
   * The StandardsApplicableReviews model module.
   * @module model/StandardsApplicableReviews
   * @version 1.0.0-private-beta
   */

  /**
   * Constructs a new <code>StandardsApplicableReviews</code>.
   * Initially, a &#39;message&#39; or response to &#39;evidence&#39; which supports a claim to a ‘standard’.  Thereafter, this will be a response to a previous message/response.
   * @alias module:model/StandardsApplicableReviews
   * @class
   * @param id {String} Unique identifier of entity
   * @param evidenceId {String} Unique identifier of associated Evidence
   * @param createdById {String} Unique identifier of Contact who created record  Derived from calling context  SET ON SERVER
   */
  var exports = function(id, evidenceId, createdById) {
    var _this = this;

    _this['id'] = id;

    _this['evidenceId'] = evidenceId;
    _this['createdById'] = createdById;



  };

  /**
   * Constructs a <code>StandardsApplicableReviews</code> from a plain JavaScript object, optionally creating a new instance.
   * Copies all relevant properties from <code>data</code> to <code>obj</code> if supplied or a new instance if not.
   * @param {Object} data The plain JavaScript object bearing properties of interest.
   * @param {module:model/StandardsApplicableReviews} obj Optional instance to populate.
   * @return {module:model/StandardsApplicableReviews} The populated <code>StandardsApplicableReviews</code> instance.
   */
  exports.constructFromObject = function(data, obj) {
    if (data) {
      obj = obj || new exports();

      if (data.hasOwnProperty('id')) {
        obj['id'] = ApiClient.convertToType(data['id'], 'String');
      }
      if (data.hasOwnProperty('previousId')) {
        obj['previousId'] = ApiClient.convertToType(data['previousId'], 'String');
      }
      if (data.hasOwnProperty('evidenceId')) {
        obj['evidenceId'] = ApiClient.convertToType(data['evidenceId'], 'String');
      }
      if (data.hasOwnProperty('createdById')) {
        obj['createdById'] = ApiClient.convertToType(data['createdById'], 'String');
      }
      if (data.hasOwnProperty('createdOn')) {
        obj['createdOn'] = ApiClient.convertToType(data['createdOn'], 'Date');
      }
      if (data.hasOwnProperty('originalDate')) {
        obj['originalDate'] = ApiClient.convertToType(data['originalDate'], 'Date');
      }
      if (data.hasOwnProperty('message')) {
        obj['message'] = ApiClient.convertToType(data['message'], 'String');
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
   * Unique identifier of previous version of entity
   * @member {String} previousId
   */
  exports.prototype['previousId'] = undefined;
  /**
   * Unique identifier of associated Evidence
   * @member {String} evidenceId
   */
  exports.prototype['evidenceId'] = undefined;
  /**
   * Unique identifier of Contact who created record  Derived from calling context  SET ON SERVER
   * @member {String} createdById
   */
  exports.prototype['createdById'] = undefined;
  /**
   * UTC date and time at which record created  Set by server when creating record  SET ON SERVER
   * @member {Date} createdOn
   */
  exports.prototype['createdOn'] = undefined;
  /**
   * UTC date and time at which record was originally created  Set by server when first creating record  SET ON SERVER
   * @member {Date} originalDate
   */
  exports.prototype['originalDate'] = undefined;
  /**
   * Serialised message data
   * @member {String} message
   */
  exports.prototype['message'] = undefined;



  return exports;
}));


