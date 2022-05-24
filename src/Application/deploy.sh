dotnet publish --configuration Release
#rsync -azP bin/Release/netcoreapp3.1/publish/ root@rauxmedia.com:/var/www/demo/cdacommercial
rsync -azP --delete bin/Release/netcoreapp3.1/publish/ -e "ssh -i ~/Documents/cdacommercial/AWS/EC2/CDA-FrontEnd.pem" ubuntu@ec2-3-128-161-245.us-east-2.compute.amazonaws.com:/var/www/cron_server/