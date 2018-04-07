///// <reference path="scripts/angular.js" />

var GettingData = angular.module("FetchingDataModule", [])
    .controller("FetchingDataController", function ($scope, $http) {
        GetData();
        //Declaring Variables for error and success callings
        var ErrorCallBack = function (reason) {
            $scope.error = reason.data;
            confirm($scope.error);
        };
        var SucessCallBack = function (response) {GetData();GetDataForCharts();};
        var SuccessCallBackForGetChartData = function (response) {
            $scope.ChartData = response.data;
        };
        var SuccessCallBackForGetData = function (response) {
            $scope.TaskList = response.data;
            GetDataForCharts();
        };

        //Functions to call Web service

        function GetData() {
            $http({
                method: 'get',
                url: 'TodoService.asmx/getData'
            })
                .then(function (response) {
                    $scope.TaskList = response.data;
                    GetDataForCharts();
                }, ErrorCallBack);
        };

        $scope.DeleteData = function (Task) {
            if ($scope.deleteConfirmation = confirm("Delete the task")) {
                $scope.deleteConfirmation = "YES";
                if ($scope.deleteConfirmation = "YES") {
                    $http({
                        method: 'post',
                        url: 'TodoService.asmx/DeleteRecord',
                        data: JSON.stringify({ Task_ID: Task.TaskID }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json"
                    })
                        .then(SucessCallBack, ErrorCallBack);
                }
            }
        };

        $scope.DeleteAllData = function () {
            if ($scope.deleteallConfirmation = confirm("Delete all tasks")) {
                $scope.deleteallConfirmation = "YES";
                if ($scope.deleteallConfirmation = "YES") {
                    $http({
                        method: 'Get',
                        url: 'TodoService.asmx/DeleteAllTasks'
                    })
                        .then(SucessCallBack, ErrorCallBack);
                };
            }
        };

        $scope.StatusChange = function (Task) {
            $http({
                method: 'post',
                url: 'TodoService.asmx/UpdateRecord',
                data: JSON.stringify(Task),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            })
                .then(SucessCallBack, ErrorCallBack);
        };

        $scope.InsertTask = function (InsertTaskName) {
            $http({
                method: 'post',
                url: 'TodoService.asmx/InsertRecord',
                data: JSON.stringify({ Task_Name: InsertTaskName }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            })
                .then(SucessCallBack, ErrorCallBack);
            $('#NewTaskPlaceHolder').val("");
        };

        function GetDataForCharts() {
            $http({
                method: 'get',
                url: 'TodoService.asmx/getDataForCharts'
            })
                .then(SuccessCallBackForGetChartData, ErrorCallBack);
        };
       
        //Pagination functions

    });