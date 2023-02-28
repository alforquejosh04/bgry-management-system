var app = angular.module("barangaymanagementsystem.directives");

app.directive("formOnChange", function ($parse) {
    return {
        require: 'form',
        link: function (scope, element, attrs) {
            var cb = $parse(attrs.formOnChange);
            element.on('change', function () {
                cb(scope);
            });
        }
    }
});