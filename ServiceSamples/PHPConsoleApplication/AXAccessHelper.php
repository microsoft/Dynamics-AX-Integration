<?php
	//Require other files.
	require_once 'Settings.php';
	
	class AXAccessHelper
	{
		// Add required headers like authorization header, service version etc.
		public static function AddRequiredHeadersAndSettings($ch, $postData = '')
		{
			//Generate the authentication header
			$authHeader = self::getAuthenticationHeader(Settings::$appTenantDomainName);
			// Add authorization header, request/response format header( for json) and a header to request content for Update and delete operations.  
			curl_setopt($ch, CURLOPT_HTTPHEADER, array($authHeader,  'Accept:application/json;odata=minimalmetadata',
														'Content-Type:application/json;odata=minimalmetadata', 'Prefer:return-content',
														'Content-Length: ' . strlen($postData)));
            if ($postData != '') {
				curl_setopt($ch, CURLOPT_POSTFIELDS, $postData);
			}
			// Set the option to recieve the response back as string.
			curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1); 
			// By default https does not work for CURL.
			curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
		}
		
		public static function getAuthenticationHeader($appTenantDomainName){
			//resource
			$appResource = urlencode(settings::$appADResource);
			//clientID
			$appClientID = urlencode(settings::$appADClientId);
			//username
			$appUserID = urlencode(settings::$appUserID);
			// Password
			$appUserPassword = urlencode(settings::$password);
			
			// Construct the body for the STS request
			$authenticationRequestBody = 'resource='.$appResource.'&client_id='.$appClientID.'&grant_type=password&username='.$appUserID.'&password='.$appUserPassword.'&scope=openid';
			
			//Using curl to post the information to STS and get back the authentication response    
			$ch = curl_init();
			// set url 
			$stsUrl = 'https://login.windows.net/'.$appTenantDomainName.'/oauth2/token?api-version=1.0';        
			curl_setopt($ch, CURLOPT_URL, $stsUrl); 
			// Get the response back as a string 
			curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1); 
			// Mark as Post request
			curl_setopt($ch, CURLOPT_POST, 1);
			// Set the parameters for the request
			curl_setopt($ch, CURLOPT_POSTFIELDS,  $authenticationRequestBody);
			
			// By default, HTTPS does not work with curl.
			curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
			// read the output from the post request
			$output = curl_exec($ch);         
			// close curl resource to free up system resources
			curl_close($ch);      
			// decode the response from sts using json decoder
			$tokenOutput = json_decode($output);
			return 'Authorization:' . $tokenOutput->{'token_type'}.' '.$tokenOutput->{'access_token'};
			//return $tokenOutput->{'token_type'}.' '.$tokenOutput->{'access_token'};
		}
	}
?>