var gulp = require("gulp"),
    concat = require("gulp-concat"),
    less = require("gulp-less"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify")(),
    pump = require("pump"),
    sequence = require("gulp-sequence"),
    merge = require("merge-stream"),
    del = require("del"),
    colors = require("colors/safe"),
    events = require("events"),
    bundleconfig = require("./bundleconfig.json");

events.EventEmitter.defaultMaxListeners = 50;

gulp.task("less-all", function () {
    return gulp.src([
        '!./Styles/variablesMain.less', 
        './Styles/*.less'])
        .pipe(less())
        .pipe(gulp.dest('./wwwroot/css'))
        .on('end', function () {
            console.log(colors.green("LESS files have been compiled"));
        });
});

gulp.task("less-mfa", function () {
    return gulp.src("./wwwroot/Content/CoreAuth/css/*.less")
        .pipe(less())
        .pipe(gulp.dest('./wwwroot/Content/CoreAuth/css'))
        .on('end', function () {
            console.log(colors.green("MFA LESS files have been compiled"));
        });
});

gulp.task('less-watch', function () {
    gulp.watch("./Styles/*.less").on("change", function (event) {
        lessCompile(event.path);
    });
});

gulp.task('mfa-less-watch', function() {
    gulp.watch("./wwwroot/Content/CoreAuth/css/*.less").on("change", function(event){
        lessMFACompile(event.path);
    });
});

gulp.task("min:css", function () {
    var tasks = getBundles(".css").map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
            .pipe(concat(bundle.outputFileName))
            .pipe(cssmin())
            .pipe(gulp.dest("."));
    });

    return merge(tasks).on('end', function () {
        console.log(colors.green("CSS has been bundled and minified"));
    });
});

gulp.task("bundle:js", function () {
    var tasks = getBundles(".js").map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
            .pipe(concat(bundle.outputFileName))
            .pipe(gulp.dest("."));
    });

    return merge(tasks).on('end', function () {
        console.log(colors.green("JS has been bundled"));
    });
});

gulp.task("compress:js", function () {
    return pump([
        gulp.src("wwwroot/js/Bundle/*.js"),
        uglify,
        gulp.dest("wwwroot/js/Bundle")
    ], function () {
        console.log(colors.green("JS has been minified"));
    });
});

gulp.task("min:js", sequence("bundle:js", "compress:js"));

gulp.task("clean", function () {
    var files = bundleconfig.map(function (bundle) {
        return bundle.outputFileName;
    });

    return del(files).then(function () {
        console.log(colors.green("Bundles have been cleaned"));
    }); 
});

gulp.task("watch", function () {
    getBundles(".js").forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["bundle:js"]);
    });

    getBundles(".css").forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:css"]);
    });
});

gulp.task("min", function (cb) {
    sequence("min:css", "min:js")(cb);
});

gulp.task("dev", function (cb) {
    sequence(["less-watch", "mfa-less-watch", "watch"])(cb);
    console.log(colors.green.underline("Gulp is polling for changes..."));
});

gulp.task("build", function () {
    sequence("clean", "less-all", "less-mfa", "min")(function () {
        console.log(colors.green("Build completed"));
    });
});

gulp.task("whitelabel", function(){
    sequence("clean", "less-all", "less-mfa", "min:css")(function () {
        console.log(colors.green("Whitelabele completed"));
    });
});

function getBundles(extension) {
    return bundleconfig.filter(function (bundle) {
        return new RegExp(`${extension}$`).test(bundle.outputFileName);
    });
}

function lessCompile(file) {
    if (file.includes('variables.less')) {
        gulp.start('less-all');
    } else {
        gulp.src([file])
            .pipe(less())
            .pipe(gulp.dest('wwwroot/css'))
            .on('end', function () {
                console.log(colors.green("Successfully compiled " + file));
            });
    }
}

function lessMFACompile(file) {
    if (file.includes('Variables.less')) {
        gulp.start('less-mfa');
    } else {
        gulp.src([file])
            .pipe(less())
            .pipe(gulp.dest('wwwroot/Content/CoreAuth/css'))
            .on('end', function () {
                console.log(colors.green("Successfully compiled " + file));
            });
    }
}
