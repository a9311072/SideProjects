for /F "tokens=2-4 delims=/ " %%i in ('date /t') do set yyyymmdd1="%%k"_"%%i"_"%%j"
mkdir .\\report\\%yyyymmdd1%

locust -f ./announcement/API_get_message.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/ann_get_message_1
locust -f ./announcement/API_get_message.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/ann_get_message_2
locust -f ./announcement/API_get_message.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/ann_get_message_3
locust -f ./announcement/API_get_permission.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/ann_get_permission_1
locust -f ./announcement/API_get_permission.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/ann_get_permission_2
locust -f ./announcement/API_get_permission.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/ann_get_permission_3
locust -f ./announcement/API_put_ack.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/ann_put_ack_1
locust -f ./announcement/API_put_ack.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/ann_put_ack_2
locust -f ./announcement/API_put_ack.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/ann_put_ack_3
locust -f ./announcement/API_put_read.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/ann_put_read_1
locust -f ./announcement/API_put_read.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/ann_put_read_2
locust -f ./announcement/API_put_read.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/ann_put_read_3

locust -f ./dashboard/get_dashboard_v1.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/dashboard_v1_1
locust -f ./dashboard/get_dashboard_v1.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/dashboard_v1_2
locust -f ./dashboard/get_dashboard_v1.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/dashboard_v1_3
locust -f ./dashboard/get_dashboard_v2.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/dashboard_v2_1
locust -f ./dashboard/get_dashboard_v2.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/dashboard_v2_2
locust -f ./dashboard/get_dashboard_v2.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/dashboard_v2_3

locust -f ./learning/API_get_course_lesson.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/learning_get_course_lesson_1
locust -f ./learning/API_get_course_lesson.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/learning_get_course_lesson_2
locust -f ./learning/API_get_course_lesson.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/learning_get_course_lesson_3
locust -f ./learning/API_get_course_list.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/learning_get_course_list_1
locust -f ./learning/API_get_course_list.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/learning_get_course_list_2
locust -f ./learning/API_get_course_list.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/learning_get_course_list_3
locust -f ./learning/API_get_lesson_page.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/learning_get_lesson_page_1
locust -f ./learning/API_get_lesson_page.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/learning_get_lesson_page_2
locust -f ./learning/API_get_lesson_page.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/learning_get_lesson_page_3

locust -f ./login/login.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/login_1
locust -f ./login/login.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/login_2
locust -f ./login/login.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/login_3

locust -f ./reward/API_get_event_reward.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/event_get_reward_1
locust -f ./reward/API_get_event_reward.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/event_get_reward_2
locust -f ./reward/API_get_event_reward.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/event_get_reward_3

locust -f ./reward/API_get_reward_history.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_history_1
locust -f ./reward/API_get_reward_history.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_history_2
locust -f ./reward/API_get_reward_history.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_history_3
locust -f ./reward/API_get_reward_reason.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_reason_1
locust -f ./reward/API_get_reward_reason.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_reason_2
locust -f ./reward/API_get_reward_reason.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_reason_3
locust -f ./reward/API_get_reward_summary.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_summary_1
locust -f ./reward/API_get_reward_summary.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_summary_2
locust -f ./reward/API_get_reward_summary.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_summary_3
locust -f ./reward/API_get_top_receiver.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_top_receiver_1
locust -f ./reward/API_get_top_receiver.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_top_receiver_2
locust -f ./reward/API_get_top_receiver.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_top_receiver_3
locust -f ./reward/API_get_top_sender.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_top_sender_1
locust -f ./reward/API_get_top_sender.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_top_sender_2
locust -f ./reward/API_get_top_sender.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_top_sender_3
locust -f ./reward/API_get_reward_unseen_count.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_unseen_count_1
locust -f ./reward/API_get_reward_unseen_count.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_unseen_count_2
locust -f ./reward/API_get_reward_unseen_count.py --no-web --run-time 3m -c 1000 -r 100 --csv=./report/%yyyymmdd1%/reward_get_unseen_count_3

pause

