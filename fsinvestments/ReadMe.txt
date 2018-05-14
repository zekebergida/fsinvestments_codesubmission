thanks for the great coding challenge
please allow me to explain a couple of hting about my code

1- for #2 on the output specs, the assets under management report, as far as i understand
assets under management would be calculated by multiplying the number of shares currently under
management for each fund by that shares CURRENT share price.
Because current share price was not given I included in the report the number of shares under management for each fund 
which could easily be translated into total $ amount undr management when the current share price for each fund is available.

2- Do to some unexpected difficulties I was not able to tackle item 3 in the specs, the break report, in time.
Please take into consideration that i am working full time and I have a newborn at home.

3-I'd like to explain why i calculated investor profit the way i did by presenting my progression of thought.
-If an investor buys 1 share @ $8/share and 1 share @ $12/share it it obvious that if he sells both @ $10/share
then he came out even

-It is therefore very reasonable to say that if he sold only 1 @ $10/share that he broke even on that share.
-From there it can be said that if he sold 1 @ $11/share then his profit from that sale was $1

-This led me to think that the way to calculate profit would simply be to find the total average sell price and total average 
buy price and multiply the number sold by the difference.

-However i realize that in the following case that formula would not make sense, If an investor bought 1 share
@ $8 dollars/share and sold it @ $10/share he clearly profited $2. It would not be reasonable to say that if he went ahead 
the next week and bought 1 share @ $12/share that his profit from the erlier sale went away.

-therefore the way i calculated profit was to assess the average cost per share of shares held AT THE TIME of the sale
and tally the gain/loss for all the sales across each fund for the total gain/loss for each fund.

Zeke B