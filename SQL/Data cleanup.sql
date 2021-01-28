use SAEIS
select Right(LinkToImage,4), count(*) from Images group by Right(LinkToImage,4)
-- Fix )jpg
select * from Images where Right(LinkToImage,4) = ')jpg'
Select Left(LinkToImage, Len(LinkToImage)-3) from Images where Right(LinkToImage,4) = ')jpg'
Update
  Images
set
  LinkToImage = Left(LinkToImage, Len(LinkToImage)-3)+'.jpg'
where
  Right(LinkToImage,4) = ')jpg'
