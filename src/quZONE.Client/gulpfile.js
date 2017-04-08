var gulp = require('gulp');
var jshint = require('gulp-jshint');
var del = require('del');
var htmlmin = require('gulp-htmlmin');
var $ = require('gulp-load-plugins')({lazy: true});

$.taskListing = require('gulp-task-listing');
$.angularTemplateCache = require('gulp-angular-templatecache');
$.inject = require('gulp-inject');
$.useref = require('gulp-useref');
$.filter = require('gulp-filter');
$.csso = require('gulp-csso');
$.concat = require('gulp-concat');
$.uglify = require('gulp-uglify');
$.rename = require('gulp-rename');
$.ngAnnotate = require('gulp-ng-annotate');

// Gulp Config

var vendorjs = {
    localstorage: '/Scripts/angular-local-storage.min.js',
    datapicker: '/Scripts/bootstrap-datepicker.js',
    validator: '/Scripts/angular-validator-min.js',
    angularclock: '/Scripts/angular-clock.js',
    logger: '/Scripts/logger.min.js',
    dialog: '/Scripts/ngDialog.min.js',
    pagination: '/Scripts/dirPagination.js',
    twilio: '/Scripts/angular-twilio.js',
    fileupload: '/Scripts/ng-file-upload.min.js',
    fileuploadshim: '/Scripts/ng-file-upload-shim.min.js',
    application: '/Scripts/application.js',
    demo: '/Scripts/demo.js'

};

var config = {
    appjs: [
        './app/**/*.js'
    ],
    fonts: './fonts/*.*',
    images: './content/images/**/*.*',
    htmlTemplates: './app/**/*.html',
    templateCache: {
        file: 'template.js',
        options: {
            module: 'qzone',
            standAlone: false,
            root: 'app/'
        }
    },
    temp: './temp/',
    content: './content/**/*.*',
    index: './index.html',
    js: [        
        './Scripts/angular-local-storage.min.js',
        './Scripts/bootstrap-datepicker.js',
        './Scripts/angular-validator-min.js',
        './Scripts/angular-clock.js',
        './Scripts/logger.min.js',
        './Scripts/ngDialog.min.js',
        './Scripts/dirPagination.js',
        './Scripts/angular-twilio.js',
        './Scripts/ng-file-upload.min.js',
        './Scripts/ng-file-upload-shim.min.js',
        './Scripts/application.js',
        './Scripts/demo.js',

        './app/app.js',
        './app/**/*.js'
    ],
    css: [        
        './content/AdminLTE.css',
        './content/skin-blue-light.css',
        './content/datepicker3.css',
        './content/ngDialog.css',
        './content/ngDialog-theme-default.min.css',
        './content/ngDialog-custom-width.css',
        './content/ngDialog-theme-plain.css',
        './content/site.css',
        './content/angular-clock.css'
        
    ],
    build: './build/'

};

// Clean up

gulp.task('clean', function(){
    return del(['./build']);
});

gulp.task('cleanTemp', function(){
    return del(['./temp']);
});

//Verify javascript errors

gulp.task('vet', function(){
    return gulp
        .src([
            './app/**/*.js',
            './*.js'
        ])
        .pipe(jshint())
        .pipe(jshint.reporter('jshint-stylish', {verbose: true}));
});

// Task lisitng

gulp.task('help', $.taskListing);

// Default task

gulp.task('default', ['help']);

// Copy assets

gulp.task('copy_fonts', function(){    
    return gulp
        .src(config.fonts)
        .pipe(gulp.dest(config.build + 'fonts'));
});

gulp.task('copyContents', function(){
    return gulp
        .src(config.images)
        .pipe(gulp.dest(config.build + 'content/images/'));
})

gulp.task('copyIndex', function(){
    return gulp
        .src(config.index)
        .pipe(gulp.dest(config.build));
})

// Load tempate cache

gulp.task('templateCache', ['cleanTemp'], function(){
    return gulp
    .src(config.htmlTemplates)    
    .pipe(htmlmin({collapseWhitespace: true}))
    .pipe($.angularTemplateCache(
        config.templateCache.file,
        config.templateCache.options
    ))
    .pipe(gulp.dest(config.temp));
})

// Optimize

gulp.task('optimize', ['inject', 'css'], function(){

    var templateCache = config.temp + config.templateCache.file;
    //console.log(templateCache);
    //var assets = $.useref.assets({searchPath:'./'});
    var cssFilter = $.filter(config.css, {restore: true});
    var jsFilter = $.filter(config.js, {restore: true});

    return gulp
        .src(config.index)
        .pipe($.inject(gulp.src(templateCache, {read: false}), {
            starttag: '<!-- inject:template:js -->'
        }))
        //.pipe(assets)
        //.pipe(assets.restore())
       /* 
        .pipe(cssFilter)
        .pipe($.csso())
        .pipe(cssFilter.restore)
        .pipe(jsFilter)
        .pipe($.ngAnnotate())
        .pipe($.uglify())
        .pipe(jsFilter.restore)

        */
        .pipe($.useref())
        .pipe(gulp.dest(config.build));

});

// Inject js

gulp.task('inject', function(){
    return gulp
        .src(config.index)
        .pipe($.inject(gulp.src(config.js)))
        .pipe(gulp.dest(config.build));
});

// Inject css

gulp.task('css', ['concatCss'], function(){
    return gulp
        .src(config.index)
        //.pipe($.inject(gulp.src(config.css)))
        .pipe($.inject(gulp.src(config.build + '/styles/app.css')))
        .pipe(gulp.dest(config.build));

});

gulp.task('cssOptimize', function(){

    var cssFilter = $.filter(config.css, {restore: true});
console.log(config.css);
    return gulp
        .src(config.index)
        .pipe(cssFilter)
        .pipe($.csso())
        .pipe(cssFilter.restore)
        .pipe(gulp.dest(config.build));
});

gulp.task('concatCss', function(){
    return gulp
        .src(config.css)
        .pipe($.concat('app.css'))
        .pipe(gulp.dest(config.build + '/styles/'))

});

//minifyJs has issues so that it will NOT be used at this version
/**/
gulp.task('minifyJs', function(){
    return gulp
        .src(config.build + 'js/main.js')
        .pipe($.ngAnnotate())
        .pipe($.uglify())
        .pipe($.rename('main.min.js'))
        .pipe(gulp.dest(config.build + 'js/'))

});


// Build for deployment

gulp.task('build', ['clean','copyContents', 'copy_fonts', 'templateCache', 'optimize']);