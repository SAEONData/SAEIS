use SAEIS
Select 
  *
from 
  EstuaryLiterature
  left join Estuary
    on (EstuaryLiterature.EstuaryID = Estuary.EstuaryID)
  left join Literature
    On (EstuaryLiterature.LiteratureID = Literature.LiteratureID)
where
  (Literature.Title is null)

Delete
  EstuaryLiterature
from
  EstuaryLiterature
  left join Estuary
    on (EstuaryLiterature.EstuaryID = Estuary.EstuaryID)
  left join Literature
    On (EstuaryLiterature.LiteratureID = Literature.LiteratureID)
where
  (Literature.Title is null)
