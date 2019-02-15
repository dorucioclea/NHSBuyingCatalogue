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
    define(['ApiClient', 'model/FileResult', 'model/PaginatedListBlobInfo', 'model/PaginatedListClaimBlobInfoMap'], factory);
  } else if (typeof module === 'object' && module.exports) {
    // CommonJS-like environments that support module.exports, like Node.
    module.exports = factory(require('../ApiClient'), require('../model/FileResult'), require('../model/PaginatedListBlobInfo'), require('../model/PaginatedListClaimBlobInfoMap'));
  } else {
    // Browser globals (root is window)
    if (!root.CatalogueApi) {
      root.CatalogueApi = {};
    }
    root.CatalogueApi.StandardsApplicableEvidenceBlobStoreApi = factory(root.CatalogueApi.ApiClient, root.CatalogueApi.FileResult, root.CatalogueApi.PaginatedListBlobInfo, root.CatalogueApi.PaginatedListClaimBlobInfoMap);
  }
}(this, function(ApiClient, FileResult, PaginatedListBlobInfo, PaginatedListClaimBlobInfoMap) {
  'use strict';

  /**
   * StandardsApplicableEvidenceBlobStore service.
   * @module api/StandardsApplicableEvidenceBlobStoreApi
   * @version 1.0.0-private-beta
   */

  /**
   * Constructs a new StandardsApplicableEvidenceBlobStoreApi. 
   * @alias module:api/StandardsApplicableEvidenceBlobStoreApi
   * @class
   * @param {module:ApiClient} [apiClient] Optional API client implementation to use,
   * default to {@link module:ApiClient#instance} if unspecified.
   */
  var exports = function(apiClient) {
    this.apiClient = apiClient || ApiClient.instance;



    /**
     * Upload a file to support a claim  If the file already exists on the server, then a new version is created
     * Server side folder structure is something like:  --Organisation  ----Solution  ------Capability Evidence  --------Appointment Management - Citizen  --------Appointment Management - GP  --------Clinical Decision Support  --------[all other claimed capabilities]  ----------Images  ----------PDF  ----------Videos  ----------RTM  ----------Misc                where subFolder is an optional folder under a claimed capability ie Images, PDF, et al
     * @param {String} claimId Unique identifier of claim
     * @param {File} file Client file path
     * @param {String} filename Server file name
     * @param {Object} opts Optional parameters
     * @param {String} opts.subFolder optional sub-folder
     * @return {Promise} a {@link https://www.promisejs.org/|Promise}, with an object containing data of type {@link 'String'} and HTTP response
     */
    this.apiStandardsApplicableEvidenceBlobStoreAddEvidenceForClaimPostWithHttpInfo = function(claimId, file, filename, opts) {
      opts = opts || {};
      var postBody = null;

      // verify the required parameter 'claimId' is set
      if (claimId === undefined || claimId === null) {
        throw new Error("Missing the required parameter 'claimId' when calling apiStandardsApplicableEvidenceBlobStoreAddEvidenceForClaimPost");
      }

      // verify the required parameter 'file' is set
      if (file === undefined || file === null) {
        throw new Error("Missing the required parameter 'file' when calling apiStandardsApplicableEvidenceBlobStoreAddEvidenceForClaimPost");
      }

      // verify the required parameter 'filename' is set
      if (filename === undefined || filename === null) {
        throw new Error("Missing the required parameter 'filename' when calling apiStandardsApplicableEvidenceBlobStoreAddEvidenceForClaimPost");
      }


      var pathParams = {
      };
      var queryParams = {
      };
      var collectionQueryParams = {
      };
      var headerParams = {
      };
      var formParams = {
        'claimId': claimId,
        'file': file,
        'filename': filename,
        'subFolder': opts['subFolder']
      };

      var authNames = ['basic', 'oauth2'];
      var contentTypes = ['multipart/form-data'];
      var accepts = ['application/json'];
      var returnType = 'String';

      return this.apiClient.callApi(
        '/api/StandardsApplicableEvidenceBlobStore/AddEvidenceForClaim', 'POST',
        pathParams, queryParams, collectionQueryParams, headerParams, formParams, postBody,
        authNames, contentTypes, accepts, returnType
      );
    }

    /**
     * Upload a file to support a claim  If the file already exists on the server, then a new version is created
     * Server side folder structure is something like:  --Organisation  ----Solution  ------Capability Evidence  --------Appointment Management - Citizen  --------Appointment Management - GP  --------Clinical Decision Support  --------[all other claimed capabilities]  ----------Images  ----------PDF  ----------Videos  ----------RTM  ----------Misc                where subFolder is an optional folder under a claimed capability ie Images, PDF, et al
     * @param {String} claimId Unique identifier of claim
     * @param {File} file Client file path
     * @param {String} filename Server file name
     * @param {Object} opts Optional parameters
     * @param {String} opts.subFolder optional sub-folder
     * @return {Promise} a {@link https://www.promisejs.org/|Promise}, with data of type {@link 'String'}
     */
    this.apiStandardsApplicableEvidenceBlobStoreAddEvidenceForClaimPost = function(claimId, file, filename, opts) {
      return this.apiStandardsApplicableEvidenceBlobStoreAddEvidenceForClaimPostWithHttpInfo(claimId, file, filename, opts)
        .then(function(response_and_data) {
          return response_and_data.data;
        });
    }


    /**
     * Download a file which is supporting a claim
     * @param {String} claimId unique identifier of solution claim
     * @param {Object} opts Optional parameters
     * @param {String} opts.uniqueId unique identifier of file
     * @return {Promise} a {@link https://www.promisejs.org/|Promise}, with an object containing data of type {@link module:model/FileResult} and HTTP response
     */
    this.apiStandardsApplicableEvidenceBlobStoreDownloadByClaimIdPostWithHttpInfo = function(claimId, opts) {
      opts = opts || {};
      var postBody = null;

      // verify the required parameter 'claimId' is set
      if (claimId === undefined || claimId === null) {
        throw new Error("Missing the required parameter 'claimId' when calling apiStandardsApplicableEvidenceBlobStoreDownloadByClaimIdPost");
      }


      var pathParams = {
        'claimId': claimId
      };
      var queryParams = {
        'uniqueId': opts['uniqueId'],
      };
      var collectionQueryParams = {
      };
      var headerParams = {
      };
      var formParams = {
      };

      var authNames = ['basic', 'oauth2'];
      var contentTypes = [];
      var accepts = ['text/plain', 'application/json', 'text/json'];
      var returnType = FileResult;

      return this.apiClient.callApi(
        '/api/StandardsApplicableEvidenceBlobStore/Download/{claimId}', 'POST',
        pathParams, queryParams, collectionQueryParams, headerParams, formParams, postBody,
        authNames, contentTypes, accepts, returnType
      );
    }

    /**
     * Download a file which is supporting a claim
     * @param {String} claimId unique identifier of solution claim
     * @param {Object} opts Optional parameters
     * @param {String} opts.uniqueId unique identifier of file
     * @return {Promise} a {@link https://www.promisejs.org/|Promise}, with data of type {@link module:model/FileResult}
     */
    this.apiStandardsApplicableEvidenceBlobStoreDownloadByClaimIdPost = function(claimId, opts) {
      return this.apiStandardsApplicableEvidenceBlobStoreDownloadByClaimIdPostWithHttpInfo(claimId, opts)
        .then(function(response_and_data) {
          return response_and_data.data;
        });
    }


    /**
     * List all claim files and sub-folders for a solution
     * @param {String} solutionId unique identifier of solution
     * @param {Object} opts Optional parameters
     * @param {Number} opts.pageIndex 1-based index of page to return.  Defaults to 1
     * @param {Number} opts.pageSize number of items per page.  Defaults to 20
     * @return {Promise} a {@link https://www.promisejs.org/|Promise}, with an object containing data of type {@link module:model/PaginatedListClaimBlobInfoMap} and HTTP response
     */
    this.apiStandardsApplicableEvidenceBlobStoreEnumerateClaimFolderTreeBySolutionIdGetWithHttpInfo = function(solutionId, opts) {
      opts = opts || {};
      var postBody = null;

      // verify the required parameter 'solutionId' is set
      if (solutionId === undefined || solutionId === null) {
        throw new Error("Missing the required parameter 'solutionId' when calling apiStandardsApplicableEvidenceBlobStoreEnumerateClaimFolderTreeBySolutionIdGet");
      }


      var pathParams = {
        'solutionId': solutionId
      };
      var queryParams = {
        'pageIndex': opts['pageIndex'],
        'pageSize': opts['pageSize'],
      };
      var collectionQueryParams = {
      };
      var headerParams = {
      };
      var formParams = {
      };

      var authNames = ['basic', 'oauth2'];
      var contentTypes = [];
      var accepts = ['application/json'];
      var returnType = PaginatedListClaimBlobInfoMap;

      return this.apiClient.callApi(
        '/api/StandardsApplicableEvidenceBlobStore/EnumerateClaimFolderTree/{solutionId}', 'GET',
        pathParams, queryParams, collectionQueryParams, headerParams, formParams, postBody,
        authNames, contentTypes, accepts, returnType
      );
    }

    /**
     * List all claim files and sub-folders for a solution
     * @param {String} solutionId unique identifier of solution
     * @param {Object} opts Optional parameters
     * @param {Number} opts.pageIndex 1-based index of page to return.  Defaults to 1
     * @param {Number} opts.pageSize number of items per page.  Defaults to 20
     * @return {Promise} a {@link https://www.promisejs.org/|Promise}, with data of type {@link module:model/PaginatedListClaimBlobInfoMap}
     */
    this.apiStandardsApplicableEvidenceBlobStoreEnumerateClaimFolderTreeBySolutionIdGet = function(solutionId, opts) {
      return this.apiStandardsApplicableEvidenceBlobStoreEnumerateClaimFolderTreeBySolutionIdGetWithHttpInfo(solutionId, opts)
        .then(function(response_and_data) {
          return response_and_data.data;
        });
    }


    /**
     * List all files and sub-folders for a claim including folder for claim
     * @param {String} claimId unique identifier of solution claim
     * @param {Object} opts Optional parameters
     * @param {String} opts.subFolder optional sub-folder under claim
     * @param {Number} opts.pageIndex 1-based index of page to return.  Defaults to 1
     * @param {Number} opts.pageSize number of items per page.  Defaults to 20
     * @return {Promise} a {@link https://www.promisejs.org/|Promise}, with an object containing data of type {@link module:model/PaginatedListBlobInfo} and HTTP response
     */
    this.apiStandardsApplicableEvidenceBlobStoreEnumerateFolderByClaimIdGetWithHttpInfo = function(claimId, opts) {
      opts = opts || {};
      var postBody = null;

      // verify the required parameter 'claimId' is set
      if (claimId === undefined || claimId === null) {
        throw new Error("Missing the required parameter 'claimId' when calling apiStandardsApplicableEvidenceBlobStoreEnumerateFolderByClaimIdGet");
      }


      var pathParams = {
        'claimId': claimId
      };
      var queryParams = {
        'subFolder': opts['subFolder'],
        'pageIndex': opts['pageIndex'],
        'pageSize': opts['pageSize'],
      };
      var collectionQueryParams = {
      };
      var headerParams = {
      };
      var formParams = {
      };

      var authNames = ['basic', 'oauth2'];
      var contentTypes = [];
      var accepts = ['application/json'];
      var returnType = PaginatedListBlobInfo;

      return this.apiClient.callApi(
        '/api/StandardsApplicableEvidenceBlobStore/EnumerateFolder/{claimId}', 'GET',
        pathParams, queryParams, collectionQueryParams, headerParams, formParams, postBody,
        authNames, contentTypes, accepts, returnType
      );
    }

    /**
     * List all files and sub-folders for a claim including folder for claim
     * @param {String} claimId unique identifier of solution claim
     * @param {Object} opts Optional parameters
     * @param {String} opts.subFolder optional sub-folder under claim
     * @param {Number} opts.pageIndex 1-based index of page to return.  Defaults to 1
     * @param {Number} opts.pageSize number of items per page.  Defaults to 20
     * @return {Promise} a {@link https://www.promisejs.org/|Promise}, with data of type {@link module:model/PaginatedListBlobInfo}
     */
    this.apiStandardsApplicableEvidenceBlobStoreEnumerateFolderByClaimIdGet = function(claimId, opts) {
      return this.apiStandardsApplicableEvidenceBlobStoreEnumerateFolderByClaimIdGetWithHttpInfo(claimId, opts)
        .then(function(response_and_data) {
          return response_and_data.data;
        });
    }
  };

  return exports;
}));
