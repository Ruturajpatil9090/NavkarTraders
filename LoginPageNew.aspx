<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPageNew.aspx.cs" Inherits="LoginPageNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Animated Content Tabs with CSS3" />
    <meta name="keywords" content="tabs, css3, transition, checked, pseudo-class, label, css-only, sibling combinator" />
    <link href="CSS/style3.css" rel="stylesheet" type="text/css" />
    <link href="CSS/demo.css" rel="stylesheet" type="text/css" />
    <script src="JS/modernizr.custom.04022.js" type="text/javascript"></script>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans+Condensed:700,300,300italic'
        rel='stylesheet' type='text/css' />
    <link href="Styles/loginPage.css" rel="stylesheet" type="text/css" />
    <link href="Styles/customerlogin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('a.login-window').click(function () {

                //Getting the variable's value from a link 
                var loginBox = $(this).attr('href');

                //Fade in the Popup
                $(loginBox).fadeIn(300);

                //Set the center alignment padding + border see css style
                var popMargTop = ($(loginBox).height() + 24) / 2;
                var popMargLeft = ($(loginBox).width() + 24) / 2;

                $(loginBox).css({
                    'margin-top': -popMargTop,
                    'margin-left': -popMargLeft
                });

                // Add the mask to body
                $('body').append('<div id="mask"></div>');
                $('#mask').fadeIn(300);

                return false;
            });

            // When clicking on the button close or the mask layer the popup closed
            $('a.close, #mask').live('click', function () {
                $('#mask , .login-popup').fadeOut(300, function () {
                    $('#mask').remove();
                });
                return false;
            });
        });</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="maindiv">
        <div id="header">
            <p id="companyname">
                gautam sugar trading company</p>
        </div>
        <div class="middle">
            <%--   <div class="about">--%>
            <div class="container">
                <section class="tabs">
	            <input id="tab-1" type="radio" name="radio-set" class="tab-selector-1" checked="checked" />
		        <label for="tab-1" class="tab-label-1">About</label>
		
	            <input id="tab-2" type="radio" name="radio-set" class="tab-selector-2" />
		        <label for="tab-2" class="tab-label-2">Services</label>
		
	            <input id="tab-3" type="radio" name="radio-set" class="tab-selector-3" />
		        <label for="tab-3" class="tab-label-3">Work</label>
			
	            <input id="tab-4" type="radio" name="radio-set" class="tab-selector-4" />
		        <label for="tab-4" class="tab-label-4">Contact</label>
            
			    <div class="clear-shadow"></div>
			
		        <div class="content">
             
			        <div class="content-1">
						<h2>About us</h2>
                        <p style="font-size:large">Gautam Sugar Trading Company is a leading sugar house situated in
Kolhapur and Pune, Maharashtra. The company has been dealing with the
supply of all over India since 1985 (30 years).</p>
						
						<p style="font-size:large">Gautam Sugar Trading Company was founded by Mr. Sumatilal Shah and has
grown tremendously under his guidance over the last 30 years. Mr
Gautam Shah and Mr Vinit Shah are now successfully managing the
company under his guidance.</p>
<p style="font-size:large">The Company deals with the purchase of sugar from sugar mills located
in Maharashtra and Karnataka and supply it to a number of corporate
companies like Britannia, ITC, Parle, GSK etc, and various other
wholesalers and merchants all over India.
</p><p style="font-size:large">The company values its clients and helps in executing their orders in
time with perfection. The value on which the company was founded has
always inspired growth and will continue to do so in future.</p>
				    </div>
			        <div class="content-2">
						<h2>Services</h2>
                        <p>Do you see any Teletubbies in here? Do you see a slender plastic tag clipped to my shirt with my name printed on it? Do you see a little Asian child with a blank expression on his face sitting outside on a mechanical helicopter that shakes when you put quarters in it? No? Well, that's what you see at a toy store. And you must think you're in a toy store, because you're here shopping for an infant named Jeb.</p>
						<h3>Excellence</h3>
						<p>Like you, I used to think the world was this great place where everybody lived by the same standards I did, then some kid with a nail showed me I was living in his world, a world where chaos rules not order, a world where righteousness is not rewarded. That's Cesar's world, and if you're not willing to play by his rules, then you're gonna have to pay the price. </p>
				    </div>
			        <div class="content-3">
						<h2>Portfolio</h2>
                        <p>The path of the righteous man is beset on all sides by the iniquities of the selfish and the tyranny of evil men. Blessed is he who, in the name of charity and good will, shepherds the weak through the valley of darkness, for he is truly his brother's keeper and the finder of lost children. And I will strike down upon thee with great vengeance and furious anger those who would attempt to poison and destroy My brothers. And you will know My name is the Lord when I lay My vengeance upon thee.</p>
						<h3>Examples</h3>
						<p>Now that we know who you are, I know who I am. I'm not a mistake! It all makes sense! In a comic, you know how you can tell who the arch-villain's going to be? He's the exact opposite of the hero. And most times they're friends, like you and me! I should've known way back when... You know why, David? Because of the kids. They called me Mr Glass. </p>
				    </div>
				    <div class="content-4">
						<h2>Contact</h2>
                        <p>You see? It's curious. Ted did figure it out - time travel. And when we get back, we gonna tell everyone. How it's possible, how it's done, what the dangers are. But then why fifty years in the future when the spacecraft encounters a black hole does the computer call it an 'unknown entry event'? Why don't they know? If they don't know, that means we never told anyone. And if we never told anyone it means we never made it back. Hence we die down here. Just as a matter of deductive logic.</p>
						<h3>Get in touch</h3>
						<p>Well, the way they make shows is, they make one show. That show's called a pilot. Then they show that show to the people who make shows, and on the strength of that one show they decide if they're going to make more shows. Some pilots get picked and become television programs. Some don't, become nothing. She starred in one of the ones that became nothing. </p>
				    </div>
		        </div>
			</section>
            </div>
            <%--   </div>--%>
            <div class="login">
                <h1>
                    Corporate Login</h1>
                <p>
                    User Name:
                    <asp:TextBox runat="server" ID="txtUserName"></asp:TextBox></p>
                <p>
                    Password: &nbsp;&nbsp;<asp:TextBox runat="server" ID="txtPassword"></asp:TextBox></p>
                <p class="submit">
                    <asp:Button runat="server" Width="80px" ID="btnLogin" Text="LOGIN" Style="color: #385c5b;" /></p>
                <p class="remember_me" id="cl">
                    <a href="#login-box" class="login-window">Customer Login</a>
                </p>
            </div>
            <%--<div class="footer">
            </div>--%>
        </div>
    </div>
    <div id="login-box" class="login-popup" style="display: none;">
        <%--<a href="#" class="close">
            <img src="close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>--%>
        <fieldset class="textbox">
            <label class="username">
                <span>Username or email</span>
                <asp:TextBox runat="server" ID="txtCustomerName"></asp:TextBox>
            </label>
            <label class="password">
                <span>Password</span>
                <asp:TextBox runat="server" ID="txtCustomerPassword" TextMode="Password"></asp:TextBox>
            </label>
            <button class="submit button" type="button">
                Sign in</button>
            <p>
                <a class="forgot" href="#">Forgot your password?</a>
            </p>
        </fieldset>
    </div>
    </form>
</body>
</html>
