--select top 5 a.au_id,s.title_id, au_fname, au_lname, phone, address, city, state, zip, contract
--from authors as a, sales as s, titleauthor as ta
--where a.au_id = ta.au_id and s.title_id = ta.title_id


--top 5 sales for this interval
select top 5 *
from sales
where ord_date >= '1994-01-01' and ord_date <= '1994-12-31'

--the problem with the phenomenical duplicates is that different authors publish a certain book the same period?

select s.title_id, qty, a.au_id, a.au_fname, a.au_lname
from sales as s, authors as a, titleauthor as ta
where s.title_id = ta.title_id and a.au_id = ta.au_id

select s.title_id, au_fname, au_lname
from authors as a, sales as s, titleauthor as ta
where a.au_id = ta.au_id and s.title_id = ta.title_id 
 and ord_date >= '1994-01-01' and ord_date <= '1994-12-31'

--orders for this interval
 select *
 from sales as s
 where ord_date >= '1994-01-01' and ord_date <= '1994-12-31'
 

--for orders in any store--

 select ord_num, stor_id, t.title
 from sales as s, titles as t
where  ord_date >= '1994-01-01' and ord_date <= '1994-12-31'
and s.title_id = t.title_id

--for specific store that i will pick from a drop down list--

 select ord_num, t.title, t.title_id
 from sales as s, titles as t
where  ord_date >= '1994-01-01' and ord_date <= '1994-12-31'
and s.title_id = t.title_id
and stor_id like 6380 --to apotelesma to ehw parei po metavliti pou ehw orisei mesw tou parakatw query


 --show names of stores for the drop down list
 select stor_name
 from stores

 --get store id from store name which i have from drop down list

 select stor_id 
 from stores
 where stor_name like 'Eric the Read Books'



