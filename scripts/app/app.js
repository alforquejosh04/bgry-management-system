angular.module('barangaymanagementsystem.controller', []);
angular.module('barangaymanagementsystem.services', []);
angular.module('barangaymanagementsystem.directives', []);
angular.module('barangaymanagementsystemapp', ['ui.router','ui.bootstrap','barangaymanagementsystem.services', 'barangaymanagementsystem.controller', 'barangaymanagementsystem.directives']).config(function ($httpProvider) {
    $httpProvider.interceptors.push(function($window){
        return {
            'request': function (config) {
                // just yours (other libs, ie pagination might conflict with
                // their own insertions)
                if (!/^\/path-to-your-templates\//.test(config.url)) {
                    return config;
                }
                // pull a # from wherever you like, maybe the window - set by
                // your backend layout engine?
                if ($window.version) {
                    if (!config.params) {
                        config.params = {};
                    }
                    if (!config.params.v) {
                        config.params.v = $window.version;
                    }
                }
                return config;
            }
        };
    });
     
});
