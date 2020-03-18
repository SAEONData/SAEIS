use SAEIS;
select count(*) Estuaries from Estuary;
select 
  count(*) Images 
from 
  Estuary 
  inner join EstuaryImage 
    on (EstuaryImage.EstuaryID = Estuary.EstuaryID)
  inner join Images 
    on (EstuaryImage.ImageID = Images.ImageID)
where
  (Images.Available <> 'N')
select 
  count(*) Litertures 
from 
  Estuary 
  inner join EstuaryLiterature 
    on (EstuaryLiterature.EstuaryID = Estuary.EstuaryID)
  inner join Literature 
    on (EstuaryLiterature.EstuaryLiteratureID = Literature.LiteratureID)
where
  (Literature.Available <> 'N')
