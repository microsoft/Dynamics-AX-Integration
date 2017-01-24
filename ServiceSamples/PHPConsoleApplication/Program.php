<?php 
	require_once 'Settings.php';
	require_once 'PHPExamples.php';  
	
	echo 'Calling OData service endpoint:'.PHP_EOL;
	$products = PHPExamples::getProducts(); 
	
	foreach ($products as $product){
		echo($product->ProductNumber.' - '.$product->ProductSearchName.PHP_EOL);
	}
	
	echo PHP_EOL.'Calling OData inventory endpoint:'.PHP_EOL;
	$inventoryData = PHPExamples::getInventory(); 
	
	foreach ($inventoryData as $inv){
		echo($inv->ProductName.' - '.$inv->AvailableOnHandQuantity.PHP_EOL);
	}
	
	echo PHP_EOL.'Calling JSON service with no parameters:'.PHP_EOL;
	echo var_dump(PHPExamples::getUserSessionInfo());
	
	echo PHP_EOL.'Calling JSON service with data contract parameter:'.PHP_EOL;
	echo var_dump(PHPExamples::setTimeZone());
?>