<?php



class dpxparser {
	
	private $contents;
	private $counter;
	
	public function loadfile($simple){
		$p = xml_parser_create();
		xml_parse_into_struct($p, $simple, $vals, $index);
		xml_parser_free($p);
		$this->contents = $vals;
		$this->counter = 0;
	}
	
	public function parse(){
		$pages = array();
		$n = 0;
		$d = 0;
		
		$file_id = $this->addNewFile(1, 'test', "DATE: not currently implemented");
		
		$current_page_id = -1;
		$current_deob_id = -1;
		
		for($i = 0; $i < sizeof($this->contents); $i++){
			if($this->contents[$i]['tag'] == "PAGE" && $this->contents[$i]['type'] == "open"){
				//print_r($this->contents[$i]);
				//$pages[$n]['page'] = $this->extract_params($this->contents[$i]);
				$current_page_id = $this->addNewPage($file_id, 
					$this->contents[$i]['attributes']['UID'], 
					$this->contents[$i]['attributes']['ONER'], 
					$this->contents[$i]['attributes']['ONERN']);
				$d = 0;
			}
			
			if($this->contents[$i]['tag'] == "PEN"){
				//print_r($this->contents[$i]);
				//$pages[$n]['pen'][] = $this->extract_params($this->contents[$i]);
				$this->addNewPen($current_page_id,
					$this->contents[$i]['attributes']['UT'],
					$this->contents[$i]['attributes']['PW'], 
					$this->contents[$i]['attributes']['PH'], 
					$this->contents[$i]['attributes']['UID'], 
					$this->contents[$i]['attributes']['DATA']);
			}
			
			
			if($this->contents[$i]['tag'] == "DEOB"  && $this->contents[$i]['type'] == "open"){
				//print_r($this->contents[$i]);
				$pages[$n]['deob'][$d]['deob'] = $this->extract_params($this->contents[$i]);
				$current_deob_id = $this->addNewDeob($current_page_id,
					$this->contents[$i]['attributes']['UT'],
					$this->contents[$i]['attributes']['UID'],
					$this->contents[$i]['attributes']['IG']);
			}
			
			
			if($this->contents[$i]['tag'] == "EDDE"){
				//print_r($this->contents[$i]);
				//$pages[$n]['deob'][$d]['edde'][] = $this->extract_params($this->contents[$i]);
				if(!isset($this->contents[$i]['attributes']['STI'])){
					//It looks like sometimes the STI attribute isn't set so we can assume a value of 0
					$this->addNewEdde($current_deob_id,
						0,
						$this->contents[$i]['attributes']['OBJID']);
				}
				else {
					$this->addNewEdde($current_deob_id,
						$this->contents[$i]['attributes']['STI'],
						$this->contents[$i]['attributes']['OBJID']);
				}
			}
			
			if($this->contents[$i]['tag'] == "DEOB"  && $this->contents[$i]['type'] == "close"){
				//print_r($this->contents[$i]);
				$current_deob_id = -1;
				$d++;
			}
			
			
			if($this->contents[$i]['tag'] == "PAGE" && $this->contents[$i]['type'] == "close"){
				//print_r($this->contents[$i]);
				$n++;
				$current_page_id = -1;
			}
		}
		
		echo "DONE!";
		//print_r($pages);
	}
	
	private function extract_params($tag){
		return $tag['attributes'];
	}
	
	private function addNewFile($class_id, $file, $date){
		$query = "INSERT INTO `files` (`class`, `filename`, `date`) VALUES ('" . $class_id . "', '" . $file . "', NOW());";
		$result = mysql_query($query);
		return mysql_insert_id();
	}
	
	private function addNewPage($file_id, $uid, $oner, $onern){
		$query = "INSERT INTO `pages` (`file`, `uid`, `oner`, `onern`) VALUES (" . $file_id . ", '" . $uid . "', '" . $oner . "', '" . $onern . "');";
		$result = mysql_query($query);
		return mysql_insert_id();
	}
	
	private function addNewPen($page_id, $ut, $pw, $ph, $uid, $data){
		$query = "INSERT INTO `pens` (`page`, `ut`, `pw`, `ph`, `uid`, `data`) VALUES (" . $page_id . ", " . $ut . ", " . $pw . ", " . $ph . ", '" . $uid . "', '" . $data . "');";
		$result = mysql_query($query);
		return mysql_insert_id();
	}
	
	private function addNewDeob($page_id, $ut, $uid, $ig){
		$query = "INSERT INTO `deob` (`page`, `ut`, `uid`, `ig`) VALUES (" . $page_id . ", " . $ut . ", '" . $uid . "', '" . $ig . "');";
		$result = mysql_query($query);
		return mysql_insert_id();
	}
	
	private function addNewEdde($deob_id, $sti, $objid){
		$query = "INSERT INTO `edde` (`deob`, `sti`, `objid`) VALUES (" . $deob_id . ", " . $sti . ", '" . $objid . "');";
		$result = mysql_query($query);
		return mysql_insert_id();
	}
}


function open_dyknow_file($path){
	$contents = "";
	$gz = gzopen($path, 'r');
	while (!gzeof($gz)) {
		$contents .= gzgetc($gz);
	}
	gzclose($gz);
	return $contents;
}


?>