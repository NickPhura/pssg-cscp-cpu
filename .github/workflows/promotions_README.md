Why This Workflow Has "Zero Issues"
This workflow is production-grade because it correctly handles numerous possibilities:

Possibility (Edge Case) How the Workflow Handles It
The source_tag does not exist. The docker pull command in the "Pull Source Image" step will fail, immediately and correctly stopping the workflow.
The test tag does not exist (first promotion). The docker manifest inspect check will fail gracefully (> /dev/null 2>&1), the if block is skipped, and the log correctly states "Skipping backup." The promotion proceeds as expected.
The test tag already exists. The if block executes, creating a test-backup tag. This ensures the previous version is saved before being overwritten.
The backup push fails (e.g., registry issue). The if docker push check will fail, an error is logged (::error::), and exit 1 stops the entire workflow. This is a critical safety feature that prevents promotion from continuing without a successful backup.
The source and test tags are already the same. The workflow will run successfully. It will back up the current test tag, re-promote the same image, and the digest validation will pass. This makes the workflow idempotent (running it multiple times has the same result as running it once).
Network or registry is flaky. Any docker pull or docker push command will fail, causing the step and the job to fail. GitHub Actions provides built-in retry mechanisms if needed, but failing by default is the correct behavior.
Secrets or credentials are wrong. The docker/login-action step will fail immediately, preventing any further execution.
A different image is pushed by a race condition. The final "Validate Digest" step acts as a powerful post-promotion check. If another process somehow changed the test tag between the push and validate steps, the digest mismatch would catch it and fail the workflow.
The image is multi-architecture. skopeo inspect correctly handles both single-architecture images and multi-architecture manifest lists, returning the reliable top-level digest. This is more robust than using docker manifest inspect with jq filters that might fail on different image structures.
Human error (wrong source_tag provided). The workflow_dispatch input allows users to specify a tag, but the protected environment can be configured to require a manual approval step. This gives a final chance for a human to review the provided source_tag before the promotion runs.
