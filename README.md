# vcf-mailchimp
Read a bunch of VCF vCard files and build a csv with usermail-groups mapping

# Overview

This is a small utility I made to export contacts that were into an RCube WebMail account and I needed to
import into Mailchimp. Contacts were stored into groups in RCube but the group field was not exported.

What I did was export each group one by one into different vCard (*.vcf) files and then run this little
utility to generate a csv file that contained two columns: email and groups. Groups column contains a 
comma-separated list of groups into which the email belongs.

The csv format generated can be easily combined with your existing contacts (e.g.: using Excel's VLOOKUP
function). Or you can fork and modify as you need.


