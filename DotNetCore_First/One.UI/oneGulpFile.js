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

gulp.task("clean", function () {
    var files = bundleconfig.map(function (bundle) {
        return bundle.outputFileName;
    });

    return del(files).then(function () {
        console.log(colors.green("Bundles have been cleaned"));
    });
});

gulp.task("less-all", function () {
    return gulp.src([
        "!./wwwroot/less-all/variables.less",
        "./wwwroot/less-all/*.less"])
        .pipe(less())
        .pipe(gulp.dest("./wwwroot/css"))
        .on("end", function () {
            console.log(colors.green("LESS files have been compiled"));
        });
});

gulp.task("less-core", function () {
    return gulp.src([
        "!./wwwroot/less-core/variables.less",
        "./wwwroot/less-core/*.less"])
        .pipe(less())
        .pipe(gulp.dest("./wwwroot/css-core"))
        .on("end", function () {
            console.log(colors.green("Core LESS files have been compiled"));
        });
});

gulp.task("less-watch", function () {
    gulp.watch("./wwwroot/less/*.less").on("change", function (event) {
        lessCompile(event.path);
    });
});

gulp.task("core-less-watch", function() {
    gulp.watch("./wwwroot/less-core/*.less").on("change", function(event){
        lessCoreCompile(event.path);
    });
});

gulp.task("min:css", function () {
    var tasks = getBundles(".css").map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
            .pipe(concat(bundle.outputFileName))
            .pipe(cssmin())
            .pipe(gulp.dest("."));
    });

    return merge(tasks).on("end", function () {
        console.log(colors.green("CSS has been bundled and minified"));
    });
});

gulp.task("bundle:js", function () {
    var tasks = getBundles(".js").map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
            .pipe(concat(bundle.outputFileName))
            .pipe(gulp.dest("."));
    });

    return merge(tasks).on("end", function () {
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
    sequence(["less-watch", "core-less-watch", "watch"])(cb);
    console.log(colors.green.underline("Gulp is polling for changes..."));
});

gulp.task("build", function () {
    sequence("clean", "less-all", "less-core", "min")(function () {
        console.log(colors.green("Build completed"));
    });
});

gulp.task("whitelabel", function(){
    sequence("clean", "less-all", "less-core", "min:css")(function () {
        console.log(colors.green("Whitelabele completed"));
    });
});

function getBundles(extension) {
    return bundleconfig.filter(function (bundle) {
        return new RegExp(`${extension}$`).test(bundle.outputFileName);
    });
}

function lessCompile(file) {
    if (file.includes("variables.less")) {
        gulp.start("less-all");
    } else {
        gulp.src([file])
            .pipe(less())
            .pipe(gulp.dest("wwwroot/css"))
            .on("end", function () {
                console.log(colors.green("Successfully compiled " + file));
            });
    }
}

function lessCoreCompile(file) {
    if (file.includes("variables.less")) {
        gulp.start("less-core");
    } else {
        gulp.src([file])
            .pipe(less())
            .pipe(gulp.dest("wwwroot/css-core"))
            .on("end", function () {
                console.log(colors.green("Successfully compiled " + file));
            });
    }
