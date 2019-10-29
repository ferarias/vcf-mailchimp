# vcf-mailchimp
Read a bunch of VCF vCard files and build a csv with usermail-groups mapping

# Overview

This is a small utility I made to export contacts that were into an [RoundCube WebMail](https://roundcube.net/) account and I needed to
import into [Mailchimp](https://mailchimp.com/). Contacts were stored into groups in RoundCube but the group field was not exported (maybe it's possible in future versions of RoudCube, but mine was pretty old).

What I did was export each group one by one into different vCard (\*.vcf) files and then run this little
utility to generate a csv file that contained two columns: email and groups. Groups column contains a 
comma-separated list of groups into which the email belongs.

The csv format generated can be easily combined with your existing contacts (e.g.: using Excel's [VLOOKUP](https://support.office.com/en-us/article/vlookup-function-0bbc8083-26fe-4963-8ab8-93a18ad188a1)
function). Or you can fork and modify as you need.

An example formula I used for this:

    =VLOOKUP(A2, contacts_table, 2, FALSE)
    
Enjoy!
