#include <TutorialUtils.h>

#include <NCore.h>
#include <NCluster.h>

const NChar title[] = N_T("ServerStatus");
const NChar description[] = N_T("Displays various information about a matching server and nodes");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

const char defaultPort[] = "24932";

admin_handle_t* send_receive_request(cluster_socket_t *sock, admin_handle_t *send_handle);
const char *cluster_node_state_to_string(cluster_node_state_e node_state);

int usage()
{
	printf("usage:\n");
	printf("\t%s [server[:port]] ...\n", title);
	printf("\n");
	printf("\tserver  - server address.\n");
	printf("\tport    - (optional) server port.\n");

	return 1;
}

int main(int argc, char **argv)
{
	NResult res = N_OK;
	char server[96];
	char port[32];
	char *separator;
	int ret = 0;
	admin_handle_t *send_handle = NULL;
	admin_handle_t *receive_handle = NULL;
	cluster_status_code_e status_code;
	cluster_socket_t *sock = NULL;

	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 2)
	{
		OnExit();
		return usage();
	}

	/* parse address port */
	separator = strchr(argv[1], ':');
	if (separator)
	{
		size_t addressLength;
		size_t portLength;

		addressLength = separator - argv[1];
		portLength = strlen(argv[1]) - addressLength - 1;
		strncpy(server, argv[1], addressLength);
		server[addressLength] = '\0';
		strncpy(port, argv[1] + addressLength + 1, portLength);
		port[portLength] = '\0';
	}
	else
	{
		strncpy(server, argv[1], sizeof(server));
		server[sizeof(server)-1] = '\0';
		strcpy(port, defaultPort);
	}

	printf("asking info from %s:%s...\n\n", server, port);

	ret = cluster_initialize();
	if (ret != CLUSTER_OK)
	{
		printf("error in cluster_initialize, error code: %d\n", ret);
		res = ret;
		goto FINALLY;
	}

	sock = cluster_sock_connect(&status_code, server, port);
	if (status_code)
	{
		printf("cluster_sock_connect failed, error %d\n", status_code);
		res = status_code;
		goto FINALLY;
	}

	printf("requesting info about server ...\n");

	send_handle = initialize_admin_handle();
	if (!send_handle)
	{
		printf("initialize_admin_handle() failed\n");
		res = N_E_FAILED;
		goto FINALLY;
	}

	status_code = cluster_admin_server_info_req(send_handle);
	if (status_code != CLUSTER_OK)
	{
		printf("cluster_admin_server_info_req failed, error %d\n", status_code);
		res = status_code;
		goto FINALLY;
	}

	receive_handle = send_receive_request(sock, send_handle);

	status_code = finalize_admin_handle(send_handle);
	send_handle = NULL;
	if (status_code)
	{
		printf("finalize_admin_handle failed, error %d\n", status_code);
		res = status_code;
		goto FINALLY;
	}

	if (receive_handle)
	{
		int server_status;
		const char *status_comment = "";

		status_code = cluster_admin_get_server_info(receive_handle, &server_status);
		if (status_code != CLUSTER_OK)
		{
			printf("failed to receive server info\n");
			res = status_code;
			goto FINALLY;
		}

		switch (server_status)
		{
		case 2:
			status_comment = "error";
			break;
		case 1:
			status_comment = "ready";
			break;
		case 0:
			status_comment = "preparing";
			break;
		}

		printf("server status: %d (%s)\n", server_status, status_comment);

		status_code = finalize_admin_handle(receive_handle);
		receive_handle = NULL;
		if (status_code)
		{
			printf("finalize_admin_handle failed, error %d\n", status_code);
			res = status_code;
			goto FINALLY;
		}
	}
	else
	{
		printf("failed to receive server info\n");
		res = N_E_FAILED;
		goto FINALLY;
	}

	printf("\nrequesting info about nodes ...\n");

	send_handle = initialize_admin_handle();
	if (!send_handle)
	{
		printf("initialize_admin_handle failed\n");
		res = N_E_FAILED;
		goto FINALLY;
	}

	status_code = cluster_admin_nodes_info_req(send_handle);
	if (status_code)
	{
		printf("cluster_admin_nodes_info_req failed, error %d\n", status_code);
		res = status_code;
		goto FINALLY;
	}

	receive_handle = send_receive_request(sock, send_handle);

	status_code = finalize_admin_handle(send_handle);
	send_handle = NULL;
	if (status_code)
	{
		printf("finalize_admin_handle failed, error %d\n", status_code);
		res = status_code;
		goto FINALLY;
	}

	if (receive_handle)
	{
		int i;
		int nodes_count;
		int node_id;
		cluster_node_state_e node_state;

		status_code = cluster_get_admin_nodes_info_count(receive_handle, &nodes_count);
		if (status_code)
		{
			printf("cluster_get_admin_nodes_info_count failed, error %d\n", status_code);
			res = status_code;
			goto FINALLY;
		}

		printf("%d node(s) running:\n", nodes_count);
		for (i = 0; i < nodes_count; i++)
		{
			status_code = cluster_get_admin_nodes_info(receive_handle, i, &node_id, &node_state);
			if (status_code)
			{
				printf("cluster_get_admin_nodes_info failed, error %d\n", status_code);
				res = status_code;
				goto FINALLY;
			}

			printf("%d (%s)\n", node_id, cluster_node_state_to_string(node_state));
		}

		printf("\n");

		status_code = finalize_admin_handle(receive_handle);
		receive_handle = NULL;
		if (status_code)
		{
			printf("finalize_admin_handle failed, error %d\n", status_code);
			res = status_code;
			goto FINALLY;
		}
	}
	else
	{
		printf("failed to receive tasks info\n");
	}

	printf("requesting info about tasks ...\n");

	send_handle = initialize_admin_handle();
	if (!send_handle)
	{
		printf("initialize_admin_handle failed\n");
		res = N_E_FAILED;
		goto FINALLY;
	}

	status_code = cluster_admin_tasks_complete_info_req(send_handle);
	if (status_code)
	{
		printf("cluster_admin_tasks_complete_info_req failed, error %d\n", status_code);
		res = status_code;
		goto FINALLY;
	}

	receive_handle = send_receive_request(sock, send_handle);

	status_code = finalize_admin_handle(send_handle);
	send_handle = NULL;
	if (status_code)
	{
		printf("finalize_admin_handle failed, error %d\n", status_code);
		res = status_code;
		goto FINALLY;
	}

	if (receive_handle)
	{
		int i, j;
		int info_count;
		int task_id;
		int nodes_completed;
		int working_nodes;
		int task_progress;
		int node_id;
		int node_progress;

		status_code = cluster_get_admin_tasks_complete_info_count(receive_handle, &info_count);
		if (status_code)
		{
			printf("cluster_get_admin_tasks_complete_info_count failed, error %d\n", status_code);
			res = status_code;
			goto FINALLY;
		}

		printf("%d task(s):\n", info_count);
		for (i = 0; i < info_count; i++)
		{
			status_code = cluster_get_admin_tasks_complete_info(receive_handle, i, &task_id, &nodes_completed, &working_nodes, &task_progress);
			if (status_code)
			{
				printf("cluster_get_admin_tasks_complete_info failed, error %d\n", status_code);
				res = status_code;
				goto FINALLY;
			}

			printf("\tid: %d\n", task_id);
			printf("\tprogress: %d\n", task_progress);
			printf("\tnodes completed: %d\n", nodes_completed);
			printf("\tworking nodes: %d\n", working_nodes);

			for (j = 0; j < working_nodes; j++)
			{
				status_code = cluster_get_admin_tasks_complete_info_node_info(receive_handle, i, j, &node_id, &node_progress);
				if (status_code)
				{
					printf("cluster_get_admin_tasks_complete_info_node_info failed, error %d\n", status_code);
					res = status_code;
					goto FINALLY;
				}

				printf("\t\tnode ID: %d\n", node_id);
				printf("\t\tnode progress: %d\n", node_progress);
			}

			printf("\n");
		}

		printf("\n");

		status_code = finalize_admin_handle(receive_handle);
		receive_handle = NULL;
		if (status_code)
		{
			printf("finalize_admin_handle failed, error %d\n", status_code);
			res = status_code;
			goto FINALLY;
		}
	}
	else
	{
		printf("failed to receive tasks info\n");
	}

	printf("requesting info about results ...\n");

	send_handle = initialize_admin_handle();
	if (!send_handle)
	{
		printf("initialize_admin_handle failed\n");
		res = N_E_FAILED;
		goto FINALLY;
	}

	status_code = cluster_admin_results_info_req(send_handle);
	if (status_code)
	{
		printf("cluster_admin_results_info_req failed, error %d\n", status_code);
		res = status_code;
		goto FINALLY;
	}

	receive_handle = send_receive_request(sock, send_handle);

	status_code = finalize_admin_handle(send_handle);
	send_handle = NULL;
	if (status_code)
	{
		printf("finalize_admin_handle failed, error %d\n", status_code);
		res = status_code;
		goto FINALLY;
	}

	if (receive_handle)
	{
		int i;
		int results_count;
		int result_id;

		status_code = cluster_get_admin_results_count(receive_handle, &results_count);
		if (status_code)
		{
			printf("cluster_get_admin_results_count failed, error %d\n", status_code);
			res = status_code;
			goto FINALLY;
		}

		printf("%d completed task(s):\n", results_count);
		for (i = 0; i < results_count; i++)
		{
			status_code = cluster_get_admin_results_id(receive_handle, i, &result_id);
			if (status_code)
			{
				printf("cluster_get_admin_results_id failed, error %d\n", status_code);
				res = status_code;
				goto FINALLY;
			}

			printf("%d\n", result_id);
		}

		printf("\n");

		status_code = finalize_admin_handle(receive_handle);
		receive_handle = NULL;
		if (status_code)
		{
			printf("finalize_admin_handle failed, error %d\n", status_code);
			res = status_code;
			goto FINALLY;
		}
	}
	else
	{
		printf("failed to receive results info.\n");
	}

	res = N_OK;

FINALLY:
	if (send_handle)
	{
		finalize_admin_handle(send_handle);
	}

	if (receive_handle)
	{
		finalize_admin_handle(receive_handle);
	}

	if (sock)
	{
		cluster_sock_close(sock);
	}

	cluster_finalize();

	OnExit();

	return res;
}

admin_handle_t* send_receive_request(cluster_socket_t *sock, admin_handle_t *send_handle)
{
	admin_handle_t *receive_handle;
	cluster_status_code_e status_code;

	receive_handle = initialize_admin_handle();
	if (!receive_handle)
	{
		printf("initialize_admin_handle failed\n");
		return NULL;
	}

	status_code = cluster_packet_send(send_handle, sock);
	if (status_code)
	{
		printf("cluster_packet_send failed, error %d\n", status_code);
		return NULL;
	}

	status_code = cluster_packet_recv(receive_handle, sock);
	if (status_code)
	{
		printf("cluster_packet_recv failed, error %d\n", status_code);
		return NULL;
	}

	return receive_handle;
}

const char *cluster_node_state_to_string(cluster_node_state_e node_state)
{
	switch (node_state)
	{
	case CLUSTER_NODE_STARTING:
		return "starting";
	case CLUSTER_NODE_READY:
		return "ready";
	case CLUSTER_NODE_REMOVING:
		return "removing";
	default:
		return "n/a";
	}
}
