﻿
<!DOCTYPE html>
<html lang="en">
  <head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"><!-- End Required meta tags -->
    <!-- Begin SEO tag -->
    <title> Maintenance | Looper - Bootstrap 4 Admin Theme </title>
    <meta property="og:title" content="Maintenance">
    <meta name="author" content="Beni Arisandi">
    <meta property="og:locale" content="en_US">
    <meta name="description" content="Responsive admin theme build on top of Bootstrap 4">
    <meta property="og:description" content="Responsive admin theme build on top of Bootstrap 4">
    <link rel="canonical" href="https://uselooper.com">
    <meta property="og:url" content="https://uselooper.com">
    <meta property="og:site_name" content="Looper - Bootstrap 4 Admin Theme">
    <script type="application/ld+json">
      {
        "name": "Looper - Bootstrap 4 Admin Theme",
        "description": "Responsive admin theme build on top of Bootstrap 4",
        "author":
        {
          "@type": "Person",
          "name": "Beni Arisandi"
        },
        "@type": "WebSite",
        "url": "",
        "headline": "Maintenance",
        "@context": "http://schema.org"
      }
    </script><!-- End SEO tag -->
    <!-- Favicons -->
    <link rel="apple-touch-icon" sizes="144x144" href="assets/apple-touch-icon.png">
    <link rel="shortcut icon" href="assets/favicon.ico">
    <meta name="theme-color" content="#3063A0"><!-- Google font -->
    <link href="https://fonts.googleapis.com/css?family=Fira+Sans:400,500,600" rel="stylesheet"><!-- End Google font -->
    <!-- BEGIN PLUGINS STYLES -->
    <link href="Content/Theme/vendor/fontawesome-free/css/all.css" rel="stylesheet" />
    <!-- BEGIN THEME STYLES -->
        <link rel="stylesheet" href="Content/Theme/theme.css" data-skin="default">
    <link rel="stylesheet" href="Content/Theme/theme-dark.min.css" data-skin="dark">   
    <script>
      var skin = localStorage.getItem('skin') || 'default';
      var isCompact = JSON.parse(localStorage.getItem('hasCompactMenu'));
      var disabledSkinStylesheet = document.querySelector('link[data-skin]:not([data-skin="' + skin + '"])');
      // Disable unused skin immediately
      disabledSkinStylesheet.setAttribute('rel', '');
      disabledSkinStylesheet.setAttribute('disabled', true);
      // add flag class to html immediately
      if (isCompact == true) document.querySelector('html').classList.add('preparing-compact-menu');
    </script><!-- END THEME STYLES -->
  </head>
  <body>
    <!--[if lt IE 10]>
    <div class="page-message" role="alert">You are using an <strong>outdated</strong> browser. Please <a class="alert-link" href="http://browsehappy.com/">upgrade your browser</a> to improve your experience and security.</div>
    <![endif]-->
    <!-- .auth -->
    <main class="auth">
      <!-- .empty-state -->
      <div class="empty-state">
        <!-- .empty-state-container -->
        <div class="empty-state-container">
          <!-- .card -->
          <div class="card border border-primary">
            <!-- .card-body -->
            <div class="card-body">
              <div class="state-figure">
                <img class="img-fluid w-75" src="Content/Theme/images/avatars/img-5.png" alt="">
              </div>
              <h3 class="state-header"> Error 404 </h3>
              <p class="state-description"> We apologize for any inconvenience, but we'll be back up in no time. Check back soon! </p>
             <div class="state-action">
                <a href="LogIn.aspx" class="btn btn-lg btn-light"><i class="fa fa-angle-right"></i> Go Back</a>
              </div>
            </div><!-- /.card-body -->
          </div><!-- /.card -->
          <p class="text-muted small"> Are you site owner? <a href="auth-signin-v1.html">Login here</a> or enter your password. </p>
        </div><!-- /.empty-state-container -->
      </div><!-- /.empty-state -->
    </main><!-- /.auth -->
    <!-- BEGIN BASE JS -->
       <script src="Content/Theme/vendor/jquery/jquery.min.js"></script>
      <script src="Content/Theme/vendor/bootstrap/js/popper.min.js"></script>
    <script src="Content/Theme/vendor/bootstrap/js/bootstrap.min.js"></script> <!-- END BASE JS -->
    <!-- BEGIN THEME JS -->
        <script src="Content/Theme/theme.min.js"></script> <!-- END THEME JS -->
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-116692175-1"></script>
    <script>
      window.dataLayer = window.dataLayer || [];

      function gtag()
      {
        dataLayer.push(arguments);
      }
      gtag('js', new Date());
      gtag('config', 'UA-116692175-1');
    </script>
  </body>
</html>