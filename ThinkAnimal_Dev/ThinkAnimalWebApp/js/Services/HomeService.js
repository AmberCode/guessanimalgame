'use strict';

angular.module('thinkAnimalService', [])

.service('homeService', ['$http', function ($http) {

    this.GetNode = function (searchNodeModel) {

        searchNodeModel.ParentNodeId = searchNodeModel.ParentNodeId;
        searchNodeModel.Type = searchNodeModel.Type;

         return $http({
            url: webApiUrlBase + 'Nodes',
            method: 'GET',
            params: searchNodeModel
        });
    }

    this.PostNode = function (nodeModel) {
        return $http({
            url: webApiUrlBase + 'Nodes',
            method: 'POST',
            params: nodeModel
        });
    }

}]);