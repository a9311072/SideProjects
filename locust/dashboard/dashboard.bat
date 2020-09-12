for /F "tokens=2-4 delims=/ " %%i in ('date /t') do set yyyymmdd1="%%k"_"%%i"_"%%j"
mkdir ..\\report\\%yyyymmdd1%

locust -f dashboard_v1.py --no-web --run-time 2m -c 3000 -r 100 --csv=../report/%yyyymmdd1%/dashboard_v1_1
locust -f dashboard_v1.py --no-web --run-time 2m -c 3000 -r 100 --csv=../report/%yyyymmdd1%/dashboard_v1_2
locust -f dashboard_v1.py --no-web --run-time 2m -c 3000 -r 100 --csv=../report/%yyyymmdd1%/dashboard_v1_3
locust -f dashboard_v2.py --no-web --run-time 2m -c 3000 -r 100 --csv=../report/%yyyymmdd1%/dashboard_v2_1
locust -f dashboard_v2.py --no-web --run-time 2m -c 3000 -r 100 --csv=../report/%yyyymmdd1%/dashboard_v2_2
locust -f dashboard_v2.py --no-web --run-time 2m -c 3000 -r 100 --csv=../report/%yyyymmdd1%/dashboard_v2_3
