use SAEIS;
with Dups
as
(
select 
  EstuaryID, ImageID, 
  Row_Number() over (partition by EstuaryID, ImageID order by EstuaryID, ImageID) RowNum 
from 
  EstuaryImage 
)
Delete Dups where RowNum > 1

--select 
--  EstuaryID, ImageID, Count(*) 
--from 
--  EstuaryImage 
--group by 
--  EstuaryID, ImageID
--having
--  Count(*) > 1
--order by 
--  EstuaryID, ImageID