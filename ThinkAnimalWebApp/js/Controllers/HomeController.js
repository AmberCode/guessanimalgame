angular.module('thinkAnimalController', [])

.controller('homeController', ['$http', '$scope', 'homeService', function ($http, $scope, homeService) {

//Init
StartGame();

//Functions
function StartGame() {
    $scope.showStartButton = false;
    $scope.node = undefined;
    $scope.newNode = new NewNodeModel();
    $scope.isButtonDisabled = true;
    $scope.loadNode = false;
}

function GetNode(searchNodeModel) {

    $scope.isButtonDisabled = true;
    $scope.loadNode = true;

    $scope.newNode.Type = searchNodeModel.Type;
    $scope.newNode.ParentNodeId = searchNodeModel.ParentNodeId;

    homeService.GetNode(searchNodeModel)
    .success(function (data) {
        console.log(data);
        if (data.Id > 0) {
            $scope.node = data;
            $scope.isButtonDisabled = false;
            $scope.loadNode = false;
        }
        else {
            OpenNewNodeModal();
        }
        
    })
    .error(function (error) {
        console.log(error);
        $scope.status = 'Unable to get node: ' + error.message;
    })
    .finally(function () {
        console.log('GetNode_finally');
    });
}

function PostNode(nodeModel) {
    homeService.PostNode($scope.newNode)
    .success(function (data) {
        console.log(data);

        if (data == true) {
            HideNewNodeModal();
            StartGame();
        }
    })
    .error(function (error) {
        console.log(error);
        $scope.status = 'Unable to post node: ' + error.message;
    })
    .finally(function () {
        console.log('PostNode_finally');
    });
}

function OpenGuessModal() {
    $('#GuessModal').modal({ backdrop: 'static', keyboard: false });
};

function HideGuessModal() {
    $('#GuessModal').modal('hide');
}

function OpenNewNodeModal() {
    $('#NewNodeModal').modal({ backdrop: 'static', keyboard: false });
};

function HideNewNodeModal() {
    $('#NewNodeModal').modal('hide');
}
 
//Events

$scope.StartThinkAnimal = function () {
    console.log('StartThinkAnimal_Click');

    var searchNodeModel = new SearchNodeModel();

    if ($scope.node == undefined) {
        searchNodeModel.Type = false;
        searchNodeModel.ParentNodeId = 0;
    }
    else {
        searchNodeModel.Type = false;
        searchNodeModel.ParentNodeId = $scope.node.Id;
    }

    GetNode(searchNodeModel);

    $scope.showStartButton = true;
};

$scope.YesButton_Click = function () {
    OpenGuessModal();     
};

$scope.NoButton_Click = function () {
    var searchNodeModel = new SearchNodeModel();

    searchNodeModel.Type = false;
    searchNodeModel.ParentNodeId = $scope.node.Id;

    GetNode(searchNodeModel);
};

$scope.open = function () {
    $scope.showModal = true;
};

$scope.ConfirmButton_Click = function () {
    alert("Congrats!!! Think off another animal.");
    HideGuessModal();
    StartGame();
};

$scope.CancelButton_Click = function() {
    HideGuessModal();

    var searchNodeModel = new SearchNodeModel();

    searchNodeModel.Type = true;
    searchNodeModel.ParentNodeId = $scope.node.Id;

    GetNode(searchNodeModel);
};

$scope.CreateNodeButton_Click = function() {
    PostNode();
};

}]);