To start the docker container set following variables:

- ENV DnsApi__Host=http://[PDNS IP or Hostname]:[PDNS Api Port default=8081]
- ENV DnsApi__Secret=[Pdns Api Secret]
- ENV Db__Server=[MySql database Ip or Hostname]
- ENV Db__Port=[MySql server Port]
- ENV Db__Database=[dyndns database]
- ENV Db__User=[dyndns username]
- ENV Db__Password=[dyndns password]