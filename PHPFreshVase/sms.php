<?php

if(isset($_POST['message']))
{
$message= $_POST['message'];


# Chemin vers fichier texte
$file ="ocs.txt";
# Ouverture en mode écriture écrasée
$fileopen=(fopen("$file",'w'));
# Ecriture de "Début du fichier" dansle fichier texte
fwrite($fileopen,$message);
# On ferme le fichier proprement
fclose($fileopen);

}

//////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////

if(isset($_POST['state']))
{

// 1 : on ouvre le fichier
$monfichier = fopen('ocs.txt', 'r');
// 2 : on lit la première ligne du fichier
$ligne = fgets($monfichier);
// 3 : quand on a fini de l'utiliser, on ferme le fichier
fclose($monfichier);
echo $ligne;
}




