<?php
	//Require other files.
	require_once 'AXAccessHelper.php';
	
	class PHPExamples
	{
		// Constructs a Http GET request to a feed passed in as paremeter.
		// Returns the json decoded respone as the objects that were recieved in feed.
		public static function getProducts(){
			// initiaze curl which is used to make the http request.
			$ch = curl_init();
			// Add authorization and other headers. Also set some common settings.
			AXAccessHelper::AddRequiredHeadersAndSettings($ch);
			// set url 
			$odataURL = Settings::$appADResource.'/data/ReleasedDistinctProducts?$filter=dataAreaId%20eq%20\'USMF\'&cross-company=true'; 
			curl_setopt($ch, CURLOPT_URL, $odataURL);
			//Enable fiddler to capture request
			//curl_setopt($ch, CURLOPT_PROXY, '127.0.0.1:8888');
			// $output contains the output string
			$output = curl_exec($ch);
			// close curl resource to free up system resources 
			curl_close($ch);      
			$jsonOutput = json_decode($output);
			// There is a field for odata metadata that we ignore and just consume the value
			return $jsonOutput->{'value'};
		}
		
		public static function getInventory(){
			// initiaze curl which is used to make the http request.
			$ch = curl_init();
			// Add authorization and other headers. Also set some common settings.
			AXAccessHelper::AddRequiredHeadersAndSettings($ch);
			// set url 
			$odataURL = Settings::$appADResource.'/data/InventorySitesOnHand?$filter=dataAreaId%20eq%20\'USMF\'%20and%20InventorySiteId%20eq%20\'2\'&cross-company=true'; 
			curl_setopt($ch, CURLOPT_URL, $odataURL);
			//Enable fiddler to capture request
			//curl_setopt($ch, CURLOPT_PROXY, '127.0.0.1:8888');
			// $output contains the output string
			$output = curl_exec($ch);
			// close curl resource to free up system resources 
			curl_close($ch);      
			$jsonOutput = json_decode($output);
			// There is a field for odata metadata that we ignore and just consume the value
			return $jsonOutput->{'value'};
		}
		
		public static function getUserSessionInfo(){
			// initiaze curl which is used to make the http request.
			$ch = curl_init();
			// Add authorization and other headers. Also set some common settings.
			AXAccessHelper::AddRequiredHeadersAndSettings($ch);
			// set url 
			$odataURL = Settings::$appADResource.'/api/services/UserSessionService/AifUserSessionService/GetUserSessionInfo'; 
			curl_setopt($ch, CURLOPT_URL, $odataURL);
			//Enable fiddler to capture request
			//curl_setopt($ch, CURLOPT_PROXY, '127.0.0.1:8888');
			curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");                                                                     
			// $output contains the output string
			$output = curl_exec($ch);
			// close curl resource to free up system resources 
			curl_close($ch);      
			$jsonOutput = json_decode($output);
			return $jsonOutput;
		}
		
		public static function setTimeZone(){
			date_default_timezone_set('UTC');
			// initiaze curl which is used to make the http request.
			$ch = curl_init();
			
			$applyTimeZoneContract = array(
				'dateTime'	 => date(DATE_RFC2822),
				'timeZoneOffset' => 3
			);
			
			$postData = json_encode($applyTimeZoneContract);
			// Add authorization and other headers. Also set some common settings.
			AXAccessHelper::AddRequiredHeadersAndSettings($ch, $postData);
			// set url 
			$odataURL = Settings::$appADResource.'/api/services/UserSessionService/AifUserSessionService/ApplyTimeZone'; 
			curl_setopt($ch, CURLOPT_URL, $odataURL);
			//Enable fiddler to capture request
			//curl_setopt($ch, CURLOPT_PROXY, '127.0.0.1:8888');
			curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");                                                                     
			// $output contains the output string
			$output = curl_exec($ch);
			// close curl resource to free up system resources 
			curl_close($ch);      
			$jsonOutput = json_decode($output);
			return $jsonOutput;
		}	
	}
?>