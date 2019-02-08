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
    define(['ApiClient', 'model/StandardsApplicableEvidence'], factory);
  } else if (typeof module === 'object' && module.exports) {
    // CommonJS-like environments that support module.exports, like Node.
    module.exports = factory(require('../ApiClient'), require('./StandardsApplicableEvidence'));
  } else {
    // Browser globals (root is window)
    if (!root.CatalogueApi) {
      root.CatalogueApi = {};
    }
    root.CatalogueApi.PaginatedListIEnumerableStandardsApplicableEvidence = factory(root.CatalogueApi.ApiClient, root.CatalogueApi.StandardsApplicableEvidence);
  }
}(this, function(ApiClient, StandardsApplicableEvidence) {
  'use strict';




  /**
   * The PaginatedListIEnumerableStandardsApplicableEvidence model module.
   * @module model/PaginatedListIEnumerableStandardsApplicableEvidence
   * @version 1.0.0-private-beta
   */

  /**
   * Constructs a new <code>PaginatedListIEnumerableStandardsApplicableEvidence</code>.
   * A paged list of objects
   * @alias module:model/PaginatedListIEnumerableStandardsApplicableEvidence
   * @class
   */
  var exports = function() {
    var _this = this;







  };

  /**
   * Constructs a <code>PaginatedListIEnumerableStandardsApplicableEvidence</code> from a plain JavaScript object, optionally creating a new instance.
   * Copies all relevant properties from <code>data</code> to <code>obj</code> if supplied or a new instance if not.
   * @param {Object} data The plain JavaScript object bearing properties of interest.
   * @param {module:model/PaginatedListIEnumerableStandardsApplicableEvidence} obj Optional instance to populate.
   * @return {module:model/PaginatedListIEnumerableStandardsApplicableEvidence} The populated <code>PaginatedListIEnumerableStandardsApplicableEvidence</code> instance.
   */
  exports.constructFromObject = function(data, obj) {
    if (data) {
      obj = obj || new exports();

      if (data.hasOwnProperty('pageIndex')) {
        obj['pageIndex'] = ApiClient.convertToType(data['pageIndex'], 'Number');
      }
      if (data.hasOwnProperty('totalPages')) {
        obj['totalPages'] = ApiClient.convertToType(data['totalPages'], 'Number');
      }
      if (data.hasOwnProperty('pageSize')) {
        obj['pageSize'] = ApiClient.convertToType(data['pageSize'], 'Number');
      }
      if (data.hasOwnProperty('items')) {
        obj['items'] = ApiClient.convertToType(data['items'], [[StandardsApplicableEvidence]]);
      }
      if (data.hasOwnProperty('hasPreviousPage')) {
        obj['hasPreviousPage'] = ApiClient.convertToType(data['hasPreviousPage'], 'Boolean');
      }
      if (data.hasOwnProperty('hasNextPage')) {
        obj['hasNextPage'] = ApiClient.convertToType(data['hasNextPage'], 'Boolean');
      }
    }
    return obj;
  }

  /**
   * 1-based index of which page this page  Defaults to 1
   * @member {Number} pageIndex
   */
  exports.prototype['pageIndex'] = undefined;
  /**
   * Total number of pages based on NHSD.GPITF.BuyingCatalog.Models.PaginatedList`1.PageSize
   * @member {Number} totalPages
   */
  exports.prototype['totalPages'] = undefined;
  /**
   * Maximum number of items in this page  Defaults to 20
   * @member {Number} pageSize
   */
  exports.prototype['pageSize'] = undefined;
  /**
   * List of items
   * @member {Array.<Array.<module:model/StandardsApplicableEvidence>>} items
   */
  exports.prototype['items'] = undefined;
  /**
   * true if there is a page of items previous to this page
   * @member {Boolean} hasPreviousPage
   */
  exports.prototype['hasPreviousPage'] = undefined;
  /**
   * true if there is a page of items after this page
   * @member {Boolean} hasNextPage
   */
  exports.prototype['hasNextPage'] = undefined;



  return exports;
}));


