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
    define(['ApiClient', 'model/CapabilitiesImplemented', 'model/Solutions', 'model/StandardsApplicable', 'model/TechnicalContacts'], factory);
  } else if (typeof module === 'object' && module.exports) {
    // CommonJS-like environments that support module.exports, like Node.
    module.exports = factory(require('../ApiClient'), require('./CapabilitiesImplemented'), require('./Solutions'), require('./StandardsApplicable'), require('./TechnicalContacts'));
  } else {
    // Browser globals (root is window)
    if (!root.CatalogueApi) {
      root.CatalogueApi = {};
    }
    root.CatalogueApi.SolutionEx = factory(root.CatalogueApi.ApiClient, root.CatalogueApi.CapabilitiesImplemented, root.CatalogueApi.Solutions, root.CatalogueApi.StandardsApplicable, root.CatalogueApi.TechnicalContacts);
  }
}(this, function(ApiClient, CapabilitiesImplemented, Solutions, StandardsApplicable, TechnicalContacts) {
  'use strict';




  /**
   * The SolutionEx model module.
   * @module model/SolutionEx
   * @version 1.0.0-private-beta
   */

  /**
   * Constructs a new <code>SolutionEx</code>.
   * An Extended Solution with its corresponding Technical Contacts, ClaimedCapability, ClaimedStandard et al
   * @alias module:model/SolutionEx
   * @class
   */
  var exports = function() {
    var _this = this;





  };

  /**
   * Constructs a <code>SolutionEx</code> from a plain JavaScript object, optionally creating a new instance.
   * Copies all relevant properties from <code>data</code> to <code>obj</code> if supplied or a new instance if not.
   * @param {Object} data The plain JavaScript object bearing properties of interest.
   * @param {module:model/SolutionEx} obj Optional instance to populate.
   * @return {module:model/SolutionEx} The populated <code>SolutionEx</code> instance.
   */
  exports.constructFromObject = function(data, obj) {
    if (data) {
      obj = obj || new exports();

      if (data.hasOwnProperty('solution')) {
        obj['solution'] = Solutions.constructFromObject(data['solution']);
      }
      if (data.hasOwnProperty('claimedCapability')) {
        obj['claimedCapability'] = ApiClient.convertToType(data['claimedCapability'], [CapabilitiesImplemented]);
      }
      if (data.hasOwnProperty('claimedStandard')) {
        obj['claimedStandard'] = ApiClient.convertToType(data['claimedStandard'], [StandardsApplicable]);
      }
      if (data.hasOwnProperty('technicalContact')) {
        obj['technicalContact'] = ApiClient.convertToType(data['technicalContact'], [TechnicalContacts]);
      }
    }
    return obj;
  }

  /**
   * Solution
   * @member {module:model/Solutions} solution
   */
  exports.prototype['solution'] = undefined;
  /**
   * A list of ClaimedCapability
   * @member {Array.<module:model/CapabilitiesImplemented>} claimedCapability
   */
  exports.prototype['claimedCapability'] = undefined;
  /**
   * A list of ClaimedStandard
   * @member {Array.<module:model/StandardsApplicable>} claimedStandard
   */
  exports.prototype['claimedStandard'] = undefined;
  /**
   * A list of TechnicalContact
   * @member {Array.<module:model/TechnicalContacts>} technicalContact
   */
  exports.prototype['technicalContact'] = undefined;



  return exports;
}));


